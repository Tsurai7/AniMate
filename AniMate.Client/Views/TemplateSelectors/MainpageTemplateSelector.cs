using Microsoft.Maui.Controls;

namespace AniMate_app.View.TemplateSelectors
{
    class MainpageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TitleTileTemplate {  get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return TitleTileTemplate;
        }
    }
}
