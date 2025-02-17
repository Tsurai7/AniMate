namespace AniMate_app.Views.TemplateSelectors;

class MainpageTemplateSelector : DataTemplateSelector
{
    public DataTemplate TitleTileTemplate {  get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return TitleTileTemplate;
    }
}

