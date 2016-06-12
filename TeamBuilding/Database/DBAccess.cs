using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamBuilding.DBInterfaces;

namespace TeamBuilding.Database
{
    public class DBAccess: ILogin, IResistTheme, IListupTheme, IResistPartisipate
    {
        string ConnectionString
        {
            get
            {
                return @"Data Source=TEST-SQL-VM01\teambuilding;Integrated Security=True;Connect Timeout=30;";
            }
        }

        /// <summary>ユーザ登録</summary>
        /// <param name="userId">ユーザID</param>
        /// <returns></returns>
        public bool LoginUser(string userId)
        {
            if (userId == null) throw new ArgumentNullException();
            bool containsUser = false;
            using (var context = new DataTeamBuildingDataContext(ConnectionString))
            {
                if (context.DatabaseExists()) context.CreateDatabase();
                using (var connection = context.Connection)
                {
                    connection.Open();

                    var u = from user in context.User
                            where user.UserId == userId
                            select user.UserId;
                    containsUser = u.Count() > 0;
                    if (!containsUser)
                    {
                        context.User.InsertOnSubmit(new User() { UserId = userId });
                        context.SubmitChanges();
                    }
                }
            }
            return containsUser;
        }

        public bool RegistTheme(string userId,string themeContent)
        {
            if (userId == null) throw new ArgumentNullException();
            if (themeContent == null) throw new ArgumentNullException();
            bool exists = false;
            bool resisted = false;
            using (var context = new DataTeamBuildingDataContext(ConnectionString))
            {
                if (context.DatabaseExists()) context.CreateDatabase();
                using (var connection = context.Connection)
                {
                    connection.Open();

                    var t = from theme in context.Theme
                            where theme.OwnerUserId == userId
                            select theme;
                    exists = t.Count() > 0;
                    if (!exists)
                    {
                        context.Theme.InsertOnSubmit(
                            new Theme() { OwnerUserId = userId, Content = themeContent }
                        );
                        context.SubmitChanges();
                        resisted = true;
                    }
                    else
                    {
                        //t.First().Content = themeContent;
                    }
                }
            }
            return resisted;
        }

        public IList<Tuple<string,string>> ListupTheme()
        {
            IList<Tuple<string, string>> list = Enumerable.Empty<Tuple<string, string>>().ToList();
            using (var context = new DataTeamBuildingDataContext(ConnectionString))
            {
                if (context.DatabaseExists()) context.CreateDatabase();
                using (var connection = context.Connection)
                {
                    connection.Open();

                    var t = from theme in context.Theme
                            select theme;
                    list = t.Select(x => Tuple.Create(x.OwnerUserId, x.Content)).ToList();
                }
            }
            return list;
        }

        public bool ResistPartisipate(string userId,string themeId)
        {
            if (userId == null) throw new ArgumentNullException();
            if (themeId == null) throw new ArgumentNullException();

            bool exists = false;
            bool resisted = false;
            using (var context = new DataTeamBuildingDataContext(ConnectionString))
            {
                if (context.DatabaseExists()) context.CreateDatabase();
                using (var connection = context.Connection)
                {
                    connection.Open();

                    var ut = from userTheme in context.UserTheme
                            where userTheme.UserId == userId
                            select userTheme;
                    exists = ut.Count() > 0;
                    if (!exists)
                    {
                        context.UserTheme.InsertOnSubmit(
                            new UserTheme() { UserId = userId, OwnerUserId = themeId }
                        );
                    }
                    else
                    {
                        ut.First().OwnerUserId = themeId;
                    }
                    context.SubmitChanges();
                    resisted = true;
                }
            }
            return resisted;
        }
    }
}