(function () {
    buildButtonList();

    function buildButtonList() {
        let list = document.getElementById('pages');
        if (pagesCount <= 20) {
            for (let i = 1; i < pagesCount; i++) {
                list.appendChild(buildPageButton(i));
            }
        } else {
            if (sectionStartPage != 1) {
                list.appendChild(buildPagePreviousSectionButton());
            }
            if (currentPage != pagesCount) {
                for (let i = sectionStartPage; i <= sectionLastPage; i++) {
                    if (i != currentPage) {
                        list.appendChild(buildPageButton(i));
                    } else {
                        list.appendChild(buildPageActiveButton(i));
                    }
                }
                if (sectionLastPage != pagesCount) {
                    list.appendChild(buildPageNextSectionButton());
                }
            } else {
                list.appendChild(buildPageButton(pagesCount));
            }
        }
    }

    function buildPageActiveButton(page) {
        let line = document.createElement('li');
        line.className = 'page-item active';

        let link = document.createElement('a');
        link.className = 'page-link';
        link.setAttribute('href', '#');
        link.innerHTML = page;

        line.appendChild(link);

        return line;
    }

    function buildPageButton(page) {
        let line = document.createElement('li');
        line.className = 'page-item';

        let link = document.createElement('a');
        link.className = 'page-link';
        link.setAttribute('href', '/Valores/GetPage?page=' + page + '&sectionStartPage=' + sectionStartPage);
        link.innerHTML = page;

        line.appendChild(link);

        return line;
    }

    function buildPagePreviousSectionButton() {
        let line = document.createElement('li');
        line.className = 'page-item';

        let link = document.createElement('a');
        link.className = 'page-link';
        link.setAttribute('href', '/Valores/GetPreviousSection?sectionStartPage=' + sectionStartPage);
        link.innerHTML = '...';

        line.appendChild(link);

        return line;
    }

    function buildPageNextSectionButton() {
        let line = document.createElement('li');
        line.className = 'page-item';

        let link = document.createElement('a');
        link.className = 'page-link';
        link.setAttribute('href', '/Valores/GetNextSection?lastSectionPage=' + sectionLastPage);
        link.innerHTML = '...';

        line.appendChild(link);

        return line;
    }
})();