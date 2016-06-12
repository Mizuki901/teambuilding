using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamBuilding.DBInterfaces;

namespace TeamBuilding.Database
{
    public class DBAccess: ILogin, IResistTheme, IListupTheme
    {
        string ConnectionString
        {
            get
            {
                return @"Data Source=TEST-SQL-VM01\teambuilding;Integrated Security=True;Connect Timeout=30;";
            }
        }

        public bool LoginUser(string userId)
        {
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
                    }
                    else
                    {
                        t.First().Content = themeContent;
                    }
                    context.SubmitChanges();
                    resisted = true;
                }
            }
            return resisted;
        }

        public IEnumerable<Tuple<string,string>> ListupTheme()
        {
            IEnumerable<Tuple<string, string>> list = Enumerable.Empty<Tuple<string, string>>();
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
    }


}