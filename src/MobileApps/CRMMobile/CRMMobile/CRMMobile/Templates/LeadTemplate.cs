using Xamarin.Forms;

namespace CRMMobile.Templates
{
    public class LeadTemplate : DataTemplateSelector
    {
        private readonly Xamarin.Forms.DataTemplate dataTemplate;

        public LeadTemplate()
        {
            dataTemplate = new Xamarin.Forms.DataTemplate(typeof(LeadCollapseTemplate));
        }

        protected override Xamarin.Forms.DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var context = container.BindingContext;
            dataTemplate.SetValue(ViewCellBase.ParentContextProperty, context);
            return dataTemplate;
        }
    }
}