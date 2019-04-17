using System.Collections.Generic;
using System.Linq;
using Android.Util;

namespace WearExample2
{
    public enum YesNoEnum { No = 0, Yes };

    public class Book
    {
        // In der Historie merken wir und die Seite (Frage) und die Antwort auf diese Frage
        private class History
        {
            public int page;
            public YesNoEnum answer;
        }

        private List<History> history;
        private List<Page> pages;

        public Book()
        {
            // Historie erzeugen
            history = new List<History>();

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
            return pages[history.Last().page];
        }

        // Zur naechsten Seite blaettern gemaess Kontext (Ja/Nein)
        public Page GetNextPage(YesNoEnum answer)
        {
            var lastInHistory = history.Last().page;
            int nextPage = pages[lastInHistory].nextIfNo;

            if (answer == YesNoEnum.Yes)
            {
                nextPage = pages[lastInHistory].nextIfYes;
            }

            Page np = pages[nextPage];

            history.Last().answer = answer; // Antwort für aktuelle Position merken

            History h = new History(); // Neue Position erzeugen und Seite merken
            h.page = nextPage;
            history.Add(h);
            return (np);
        }

        // Seite an Position i in der Historie zurueckgeben
        public Page GetPageAt(int i)
        {
            return pages[history[i].page];
        }

        // Antwort an Position i in der Historie zurueckgeben
        public YesNoEnum GetAnswerAt(int i)
        {
            return history[i].answer;
        }

        // Historie hinter der Position i löschen
        public void RemoveBehind(int i)
        {
            var behind = i + 1;
            var numberOfItems = history.Count - i - 1;
            Log.Debug("Book", string.Format("Remove {0} pages behind position {1}", numberOfItems, i));

            if (i + 1 < history.Count()) // sind wir nicht am Ende der History?
            {
                // dann den Rest abschneiden
                history.RemoveRange(behind, numberOfItems);
            }
        }

        // Anzahl Seiten in der Historie zurueckgeben
        public int NumPages()
        {
            return history.Count;
        }

        // Historie auf Anfang zurueck setzen
        public void Reset()
        {
            history.Clear();
            History h = new History();
            h.page = 0;
            history.Add(h);
        }
    }
}