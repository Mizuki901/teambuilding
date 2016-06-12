using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBuilding.DBInterfaces
{
    /// <summary>ログインの登録を行うInterface</summary>
    public interface ILogin
    {
        bool LoginUser(string userId);
    }

    /// <summary>テーマの登録を行うInterface</summary>
    public interface IResistTheme
    {
        bool RegistTheme(string userId, string themeContent);
        bool RegistThemeWithColor(string userId, string themeContent,string color);
    }

    /// <summary>テーマの一覧を取得するInterface</summary>
    public interface IListupTheme
    {
        IList<Tuple<string, string>> ListupTheme();
        IList<Tuple<string, string, string>> ListupThemeWithColor();
    }

    /// <summary>テーマへの参加登録を行うInterface</summary>
    public interface IResistPartisipate
    {
        bool ResistPartisipate(string userId,string themeId);
    }

    /// <summary>テーマの一覧を取得するInterface</summary>
    public interface IListupThemeAdmin
    {
        IList<Tuple<string, string, List<string>>> ListupTheme();
        IList<Tuple<string, string, string,List<string>>> ListupThemeWithColor();
    }

    /// <summary>DBInstance.GetXXXX().XXXX()で呼び出してください。</summary>
    public static class DBInstance
    {
        /// <summary>ログインの登録を行うInterfaceを取得</summary>
        public static ILogin GetLoginUserInterface()
        {
            return new LoginDummy();
        }
        
        /// <summary>テーマの登録を行うInterfaceを取得</summary>
        public static IResistTheme GetResistThemeInterface()
        {
            return new ResistThemeDummy();
        }
        
        /// <summary>テーマの一覧を取得するInterfaceを取得</summary>
        public static IListupTheme GetListupThemeInterface()
        {
            return new ListupThemeDummy();
        }

        /// <summary>テーマへの参加登録を行うInterface</summary>
        public static IResistPartisipate GetResistPartisipateInterface()
        {
            return new ResistPartisipateDummy();
        }

        public static IListupThemeAdmin GetListupThemeAdminInterface()
        {
            return new ListupThemeAdminDummy();
        }
    }

    /// <summary>Loginのダミークラス</summary>
    internal class LoginDummy : ILogin
    {
        internal LoginDummy() { }

        public bool LoginUser(string userId)
        {
            var user = PartisipateData.Instance.Users.Where(x => x.UserId == userId);
            if (user.Count() == 0)
            {
                PartisipateData.Instance.Users.Add(new User() { UserId = userId });
            }
            return true;
        }
    }

    internal class ResistThemeDummy : IResistTheme
    {
        internal ResistThemeDummy() { }

        public bool RegistTheme(string userId, string themeContent)
        {
            return RegistThemeWithColor(userId, themeContent, "yellow");
        }

        /// <summary>色情報（文字列）付で取得</summary>
        public bool RegistThemeWithColor(string userId, string themeContent ,string color)
        {
            var theme = PartisipateData.Instance.Themes
                .Where(x => x.OwnerUserId == userId);
            if (theme.Count() == 0)
            {
                PartisipateData.Instance.Themes.Add(
                    new Theme() { OwnerUserId = userId, Content = themeContent,Color = color }
                    );
            }
            return theme.Count() == 0;
        }
    }

    internal class ListupThemeDummy : IListupTheme
    {
        internal ListupThemeDummy() { }

        public IList<Tuple<string, string>> ListupTheme() => ListupThemeItr().ToList();
       
        public IEnumerable<Tuple<string, string>> ListupThemeItr()
        {
            yield return Tuple.Create("Theme1", "いろいろやりたい１");
            yield return Tuple.Create("Theme2", "いろいろやりたい２");
            yield return Tuple.Create("Theme3", "いろいろやりたい３");
            yield return Tuple.Create("Theme4", "いろいろやりたい４");
            yield return Tuple.Create("Theme5", "いろいろやりたい５");
            foreach (var item in PartisipateData.Instance.Themes)
            {
                yield return Tuple.Create(item.OwnerUserId, item.Content);
            }
        }

        public IList<Tuple<string, string,string>> ListupThemeWithColor() => ListupThemeWithColorItr().ToList();

        public IEnumerable<Tuple<string, string, string>> ListupThemeWithColorItr()
        {
            yield return Tuple.Create("Theme1", "いろいろやりたい１", "yellow");
            yield return Tuple.Create("Theme2", "いろいろやりたい２", "yellow");
            yield return Tuple.Create("Theme3", "いろいろやりたい３", "red");
            yield return Tuple.Create("Theme4", "いろいろやりたい４", "yellow");
            yield return Tuple.Create("Theme5", "いろいろやりたい５", "yellow");
            foreach (var item in PartisipateData.Instance.Themes)
            {
                yield return Tuple.Create(item.OwnerUserId, item.Content, item.Color);
            }
        }

    }

    internal class ResistPartisipateDummy : IResistPartisipate
    {
        internal ResistPartisipateDummy() { }

        public bool ResistPartisipate(string userId, string themeId)
        {
            var userTheme = PartisipateData.Instance.UserThemes
                .Where(x => x.UserId == userId);
            if (userTheme.Count() == 0)
            {
                PartisipateData.Instance.UserThemes.Add(
                    new UserTheme() { UserId = userId, OwnerUserId = themeId }
                    );
            }
            else
            {
                userTheme.First().OwnerUserId = themeId;
            }
            return true;
        }
    }

    internal class ListupThemeAdminDummy : IListupThemeAdmin
    {
        internal ListupThemeAdminDummy() { }

        public IList<Tuple<string, string,List<string>>> ListupTheme() => ListupThemeItr().ToList();

        public IEnumerable<Tuple<string, string, List<string>>> ListupThemeItr()
        {
            yield return Tuple.Create("Theme1", "いろいろやりたい１", new List<string>() { "UserA" });
            yield return Tuple.Create("Theme2", "いろいろやりたい２", new List<string>() { "UserB" });
            yield return Tuple.Create("Theme3", "いろいろやりたい３", new List<string>() { "UserC" });
            yield return Tuple.Create("Theme4", "いろいろやりたい４", new List<string>() { "UserD" });
            yield return Tuple.Create("Theme5", "いろいろやりたい５", new List<string>() { "UserE", "UserF", "UserG" });
            foreach (var item in PartisipateData.Instance.Themes)
            {
                var uList = PartisipateData.Instance.UserThemes
                    .Where(x => x.OwnerUserId == item.OwnerUserId)
                    .Select(x=>x.UserId).ToList();
                yield return Tuple.Create(item.OwnerUserId, item.Content, uList);
            }
        }

        public IList<Tuple<string, string, string, List<string>>> ListupThemeWithColor() => ListupThemeWithColorItr().ToList();

        public IEnumerable<Tuple<string, string, string, List<string>>> ListupThemeWithColorItr()
        {
            yield return Tuple.Create("Theme1", "いろいろやりたい１", "yellow", new List<string>() { "UserA" });
            yield return Tuple.Create("Theme2", "いろいろやりたい２", "yellow", new List<string>() { "UserB" });
            yield return Tuple.Create("Theme3", "いろいろやりたい３", "red", new List<string>() { "UserC" });
            yield return Tuple.Create("Theme4", "いろいろやりたい４", "yellow", new List<string>() { "UserD" });
            yield return Tuple.Create("Theme5", "いろいろやりたい５", "yellow", new List<string>() { "UserE","UserF","UserG" });
            foreach (var item in PartisipateData.Instance.Themes)
            {
                var uList = PartisipateData.Instance.UserThemes
                    .Where(x => x.OwnerUserId == item.OwnerUserId)
                    .Select(x => x.UserId).ToList();
                yield return Tuple.Create(item.OwnerUserId, item.Content, item.Color, uList);
            }
        }

    }

    /// <summary>メモリ上に情報を残しておく</summary>
    internal class PartisipateData
    {
        public static PartisipateData Instance { get; } = new PartisipateData();

        public List<User> Users { get; } = new List<User>();
        public List<Theme> Themes { get; } = new List<Theme>();
        public List<UserTheme> UserThemes { get; } = new List<UserTheme>();
    }

    /// <summary>User情報</summary>
    internal class User
    {
        public string UserId { get; set; } = "";
    }

    /// <summary>Theme情報</summary>
    internal class Theme
    {
        public string OwnerUserId { get; set; } = "";
        public string Content { get; set; } = "";
        public string Color { get; set; } = "";
    }

    /// <summary>UserTheme情報</summary>
    internal class UserTheme
    {
        public string UserId { get; set; } = "";
        public string OwnerUserId { get; set; } = "";
    }

}
