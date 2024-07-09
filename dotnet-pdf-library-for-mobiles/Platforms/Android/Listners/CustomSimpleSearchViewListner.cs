using PSPDFKit.UI.Search;
using PSPDFKit.UI;
using PSPDFKit.Utils;
using PSPDFKit.Document.Search;
using RectF = Android.Graphics.RectF;

namespace dotnet_pdf_library_for_mobiles.Platforms.Android.Listners
{
    internal class CustomSimpleSearchViewListner : SimpleSearchResultListener
    {
        SearchResultHighlighter _highlighter;
        PdfFragment _fragment;

        public CustomSimpleSearchViewListner(PdfFragment fragment, SearchResultHighlighter highlighter)
        {
            _fragment = fragment;
            _highlighter = highlighter;
        }

        public override void OnMoreSearchResults(IList<SearchResult> results)
        {
            _highlighter.AddSearchResults(results);
        }

        public override void OnSearchCleared()
        {
            _highlighter.ClearSearchResults();
        }

        public override void OnSearchResultSelected(SearchResult? result)
        {
            _highlighter.SetSelectedSearchResult(result);
            if (result != null)
            {
                _fragment.ScrollTo(PdfUtils.CreatePdfRectUnion(result.TextBlock.PageRects.Cast<RectF>().ToList()), result.PageIndex, 250, false);
            }
        }
    }
}
