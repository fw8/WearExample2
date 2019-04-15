namespace WearExample2
{
    public enum PageTypeEnum { Query, Answer }

    public class Page
    {
        public string text { get; set; }
        public PageTypeEnum pageType { get; set; }
        public int nextIfYes { get; set; }
        public int nextIfNo { get; set; }
    }
}