using System.Collections.Generic;

namespace WearExample2
{
    public enum YesNoEnum { No = 0, Yes };

    public class Book
    {
        private int currentPage;
        private List<Page> pages;

        public Book()
        {
            // Buch (Liste von Blaettern) erzeugen
            pages = new List<Page>();

            // Seite 0 / geschmeckt?
            pages.Add(new Page() { pageType = PageTypeEnum.Query, text = "Hat es geschmeckt?", nextIfNo = 2, nextIfYes = 1 });

            // Seite 1 / geschmeckt / ja / mehr?
            pages.Add(new Page() { pageType = PageTypeEnum.Query, text = "OK, möchtest Du mehr?", nextIfNo = 2, nextIfYes = 0 });

            // Seite 2 / geschmeckt / nein / nachtisch?
            pages.Add(new Page() { pageType = PageTypeEnum.Query, text = "Schade, trotzdem noch Nachtisch?", nextIfNo = 4, nextIfYes = 3 });

            // Seite 3 / nachtisch / ja
            pages.Add(new Page() { pageType = PageTypeEnum.Answer, text = "Alles klar, guten Appetit!", nextIfNo = 0, nextIfYes = 0 });

            // Seite 4 / nachtisch / nein
            pages.Add(new Page() { pageType = PageTypeEnum.Answer, text = "Schade!", nextIfNo = 0, nextIfYes = 0 });

            Reset(); // Buch auf Anfang setzten
        }

        // Aktuelle Seite liefern
        public Page GetCurrentPage()
        {
            return pages[currentPage];
        }

        // Zur naechsten Seite blaettern gemaess Kontext (Ja/Nein)
        public Page GetNextPage(YesNoEnum answer)
        {
            int nextPage = pages[currentPage].nextIfNo;

            if (answer == YesNoEnum.Yes)
            {
                nextPage = pages[currentPage].nextIfYes;
            }

            Page np = pages[nextPage];
            currentPage = nextPage; // Seite merken
            return (np);
        }

        // Seite x zurückgeben
        public Page GetPageAt(int x)
        {
            return pages[x];
        }

        // Anzahl Seiten zurückgeben
        public int NumPages()
        {
            return pages.Count;
        }

        // Buch auf Anfang zurueck setzen
        public void Reset()
        {
            currentPage = 0;
        }

    }
}