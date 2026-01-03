using System;
using System.Collections.Generic;
using CRMMobile.Services;
using Xamarin.Forms;
using Android.Views;
using Xamarin.Forms.Platform.Android;
using Plugin.CurrentActivity;
using CRMMobile.Droid.Services;
using Android.Widget;
using CRMMobile.Services.Models;
using System.Linq;
using Java.Lang.Reflect;
using Android.Graphics.Drawables;
using Android.Graphics;

[assembly: Dependency(typeof(PopoverMenu))]
namespace CRMMobile.Droid.Services
{
    public class PopoverMenu : IPopupMenuService
    {
        Android.Widget.PopupMenu PopupMenu;
        Android.App.Activity Activity;
        List<PopupMenuItem> _menus;

        public event EventHandler<PopupMenuItem> OnMenuItemSelected;

        public PopoverMenu()
        {
            Activity = CrossCurrentActivity.Current.Activity;
        }

        public void MenuPopover(Page page, ToolbarItem toolbarItem, List<PopupMenuItem> menus)
        {
            _menus = menus;
            if (Activity.FindViewById(Resource.Id.toolbar) == null)
                return;

            var toolbar = Activity.FindViewById(Resource.Id.toolbar) as Android.Support.V7.Widget.Toolbar;
            if (toolbar != null)
            {
                var idx = page.ToolbarItems.IndexOf(toolbarItem);
                if (toolbar.Menu.Size() > idx)
                {
                    var _menuItem = toolbar.Menu.GetItem(idx);
                    toolbar.Menu.Clear();
                    IMenuItem menuItem = toolbar.Menu.Add(toolbarItem.Text);
                    menuItem.SetEnabled(true);
                    menuItem.SetShowAsAction(ShowAsAction.Always);

                    var icon = _menuItem.Icon;
                    ImageView view = new ImageView(Activity);
                    view.SetImageDrawable(icon);
                    menuItem.SetActionView(view);

                    Typeface tf = Typeface.CreateFromAsset(Activity.Assets, "AP-Regular.ttf");
                    PopupMenu = new Android.Widget.PopupMenu(Activity, menuItem.ActionView);
                    Field field = PopupMenu.Class.GetDeclaredField("mPopup");
                    field.Accessible = true;
                    Java.Lang.Object menuPopupHelper = field.Get(PopupMenu);
                    Method setForceIcons = menuPopupHelper.Class.GetDeclaredMethod("setForceShowIcon", Java.Lang.Boolean.Type);
                    setForceIcons.Invoke(menuPopupHelper, true);

                    foreach (var menu in menus)
                    {
                        PopupMenu.Menu.Add(menu.Name).SetIcon(GetDrawable(menu.IconName)).SetShowAsAction(ShowAsAction.Always);
                    }

                    //for (int i = 0; i < PopupMenu.Menu.Size(); i++)
                    //{
                    //    var item = PopupMenu.Menu.GetItem(i);
                    //    SpannableString spannableString = new SpannableString(item.TitleFormatted);
                    //    spannableString.SetSpan(new CustomFontFace("", tf), 0, spannableString.Length(), SpanTypes.InclusiveInclusive);
                    //    item.SetTitle(spannableString);
                    //}


                    PopupMenu.MenuItemClick += PopupMenu_MenuItemClick;
                    view.Click += (o, e) =>
                    {
                        PopupMenu.Show();
                    };
                    PopupMenu.Show();
                }
            }
        }

        private void PopupMenu_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            var item = _menus.FirstOrDefault(t => t.Name.Equals(e.Item));
            OnMenuItemSelected?.Invoke(this, item);
            PopupMenu.MenuItemClick -= PopupMenu_MenuItemClick;
        }

        private Drawable GetDrawable(string imageEntryImage)
        {
            var image = ResourceManager.GetDrawable(Activity, imageEntryImage);
            // var image = ContextCompat.GetDrawable(Activity, resID);

            Bitmap bitmap = ((BitmapDrawable)image).Bitmap;
            // Scale it to 100 x 100
            Drawable newDrawable = new BitmapDrawable(Bitmap.CreateScaledBitmap(bitmap, 100, 100, true));
            return newDrawable;
        }
    }

    //public class IconFontDrawable : Drawable
    //{
    //    public static int ANDROID_ACTIONBAR_ICON_SIZE_DP = 24;

    //    private Context Context;

    //    private String Icon;

    //    private TextPaint Paint;

    //    private int Size = -1;

    //    private int Alpha = 255;
    //    public override int Opacity => 1;

    //    public IconFontDrawable(Context context, String icon, Typeface typeface)
    //    {
    //        this.Context = context;
    //        this.Icon = icon;
    //        Paint = new TextPaint();
    //        Paint.SetTypeface(typeface);
    //        Paint.SetStyle(Android.Graphics.Paint.Style.Stroke);
    //        Paint.TextAlign = Align.Center;
    //        Paint.UnderlineText = false;
    //        Paint.Color = Android.Graphics.Color.Red;
    //        Paint.AntiAlias = true;
    //    }

    //    public override void Draw(Canvas canvas)
    //    {
    //        Rect textBounds = new Rect();
    //        String textValue = Icon;
    //        Paint.GetTextBounds("yyy", 0, 1, textBounds);
    //        float textBottom = (Bounds.Height() - textBounds.Height()) / 2f + textBounds.Height() - textBounds.Bottom;
    //        canvas.DrawText(textValue, Bounds.Width() / 2f, textBottom, Paint);
    //    }

    //    public override void SetAlpha(int alpha)
    //    {
    //        Paint.Alpha = this.Alpha = alpha;
    //    }

    //    public override void SetColorFilter(ColorFilter colorFilter)
    //    {
    //        Paint.SetColorFilter(colorFilter);
    //    }
    //}

    //public class CustomFontFace : TypefaceSpan
    //{
    //    private Typeface typeface;
    //    public CustomFontFace(string family, Typeface type)
    //        : base(family)
    //    {
    //        typeface = type;
    //    }

    //    public override void UpdateDrawState(TextPaint ds)
    //    {
    //        base.UpdateDrawState(ds);

    //    }

    //    public override void UpdateMeasureState(TextPaint paint)
    //    {
    //        base.UpdateMeasureState(paint);

    //    }



    //    private void applyCustomTypeFace(Paint paint, Typeface typeface)
    //    {
    //        int oldStyle;
    //        Typeface old = paint.Typeface;
    //        if (old == null)
    //        {
    //            oldStyle = 0;
    //        }
    //        else
    //        {
    //            oldStyle = (int)old.Style;
    //        }

    //        int fake = oldStyle & ~(int)typeface.Style;
    //        if (fake != 0 && Typeface.IsBold)
    //        {
    //            paint.FakeBoldText = true;
    //        }

    //        if (fake != 0 && Typeface.IsItalic)
    //        {
    //            paint.TextSkewX = -0.25f;
    //        }
    //        paint.SetTypeface(typeface);
    //    }
    //}
}
