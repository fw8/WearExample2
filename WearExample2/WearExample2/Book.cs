using System.Collections.Generic;

namespace WearExample2
{
    public enum YesNoEnum { No = 0, Yes };

    public class Book
    {
        private int currentPage;
        private List<int> pageHistory;
        private List<YesNoEnum> answerHistory;
        private List<Page> pages;

        public Book()
        {
            // Historie erzeugen
            pageHistory = new List<int>();
            answerHistory = new List<YesNoEnum>();

            // Buch (Liste von Blaettern) erzeugen
            pages = new List<Page>
            {
                // Seite 0 / geschmeckt?
                new Page() { pageType = PageTypeEnum.Query, text = "Hat es geschmeckt?", nextIfNo = 2, nextIfYes = 1 },

                // Seite 1 / geschmeckt / ja / mehr?
                new Page() { pageType = PageTypeEnum.Query, text = "OK, möchtest Du mehr?", nextIfNo = 2, nextIfYes = 0 },

                // Seite 2 / geschmeckt / nein / nachtisch?
                new Page() { pageType = PageTypeEnum.Query, text = "Schade, trotzdem noch Nachtisch?", nextIfNo = 4, nextIfYes = 3 },

                // Seite 3 / nachtisch / ja
                new Page() { pageType = PageTypeEnum.Answer, text = "Alles klar, guten Appetit!", nextIfNo = 0, nextIfYes = 0 },

                // Seite 4 / nachtisch / nein
                new Page() { pageType = PageTypeEnum.Answer, text = "Schade!", nextIfNo = 0, nextIfYes = 0 }
            };

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
            //currentPage = nextPage; 
            pageHistory.Add(nextPage); // Seite merken
            answerHistory.Add(answer); // Antwort merken
            return (np);
        }

        // Seite an Position x in der Historie zurückgeben
        public Page GetPageAt(int i)
        {
            return pages[pageHistory[i]];
        }

        public YesNoEnum GetAnswerAt(int i)
        {
            return answerHistory[i];
        }

        // Anzahl Seiten in der Historie zurückgeben
        public int NumPages()
        {
            return pageHistory.Count;
        }

        // Buch auf Anfang zurueck setzen
        public void Reset()
        {
            currentPage = 0;
            pageHistory.Clear();
            pageHistory.Add(0);
            answerHistory.Clear();
        }

    }
}