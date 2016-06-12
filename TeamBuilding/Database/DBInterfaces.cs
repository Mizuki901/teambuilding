using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBuilding.DBInterfaces
{
    public interface ILogin
    {
        bool LoginUser(string userId);
    }

    public interface IResistTheme
    {
        bool RegistTheme(string userId, string themeContent);
    }

    public interface IListupTheme
    {
        IEnumerable<Tuple<string, string>> ListupTheme();
    }

    public static class DBInstance
    {
        public static ILogin GetLoginUserInterface()
        {
            return new LoginDummy();
        }
        public static IResistTheme GetResistThemeInterface()
        {
            return new ResistThemeDummy();
        }
        public static IListupTheme GetListupThemeInterface()
        {
            return new ListupThemeDummy();
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
        public IEnumerable<Tuple<string, string>> ListupTheme()
        {
            yield return Tuple.Create("Theme1", "いろいろやりたい１");
            yield return Tuple.Create("Theme2", "いろいろやりたい２");
            yield return Tuple.Create("Theme3", "いろいろやりたい３");
            yield return Tuple.Create("Theme4", "いろいろやりたい４");
            yield return Tuple.Create("Theme5", "いろいろやりたい５");
        }
    }

}
