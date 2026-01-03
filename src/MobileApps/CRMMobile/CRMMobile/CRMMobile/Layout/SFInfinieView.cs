using Xamarin.Forms;

namespace CRMMobile.Layout
{
    public class SFInfinieView : StackLayout
    {
        public SFInfinieView()
        {
            Spacing = 0;
            Padding = 0;
        }

        private bool IsEmptyView;

        public void AddEmtyView(DataTemplate template)
        {
            if (template == null)
                return;

            var content = template.CreateContent();
            Children.Add(content as View);
            IsEmptyView = true;
        }

        public void RemoveEmptyView()
        {
            Children.RemoveAt(0);
        }

        public void AddView(object item, DataTemplate template)
        {
            if (item == null) return;

            if (IsEmptyView)
            {
                RemoveEmptyView();
            }

            Children.Add(ViewFor(item, template));
            IsEmptyView = false;
        }

        public void UpdateView(int index, object item, DataTemplate template)
        {
            Children[index].BindingContext = item;
            //Children.Insert(index,ViewFor(item,template));
        }

        public void RemoveView(int index)
        {
            Children.RemoveAt(index);
        }

        public void RemoveAllView()
        {
            Children.Clear();
        }

        protected virtual View ViewFor(object item, DataTemplate template)
        {
            View view = null;

            if (template != null)
            {
                var content = template.CreateContent();
                view = content is View ? content as View : ((ViewCell)content).View;
                view.BindingContext = item;
            }

            return view;
        }
    }
}
