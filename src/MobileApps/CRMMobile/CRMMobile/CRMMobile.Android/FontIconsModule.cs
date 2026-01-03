using CRMMobile.Helper;
using JoanZapata.XamarinIconify;
using System.Linq;

namespace CRMMobile.Droid
{
    public class FontIconsModule : IIconFontDescriptor
    {

        public string FontFileName => "icomoon2.ttf";
        private static readonly ILookup<string, Icon> _characters = EnumToLookup.ToLookup<FontIcons>();

        public ILookup<string, Icon> Characters => _characters;
    }
}
