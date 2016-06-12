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
    }

    /// <summary>テーマの一覧を取得するInterface</summary>
    public interface IListupTheme
    {
        IList<Tuple<string, string>> ListupTheme();
    }

    /// <summary>テーマへの参加登録を行うInterface</summary>
    public interface IResistPartisipate
    {
        bool ResistPartisipate(string userId,string themeId);
    }

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
    }

    internal class LoginDummy : ILogin
    {
        internal LoginDummy() { }

        public bool LoginUser(string userId)
        {
            return true;
        }
    }

    internal class ResistThemeDummy : IResistTheme
    {
        internal ResistThemeDummy() { }

        public bool RegistTheme(string userId, string themeContent)
        {
            return true;
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
        }
    }

    internal class ResistPartisipateDummy : IResistPartisipate
    {
        internal ResistPartisipateDummy() { }


        public bool ResistPartisipate(string userId, string themeId) => true;
    }

}
