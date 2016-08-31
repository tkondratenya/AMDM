/*-------------------------------------------------
		Quick Pager jquery plugin
		
		Copyright (C) 2011 by Dan Drayne
		Permission is hereby granted, free of charge, to any person obtaining a copy
		of this software and associated documentation files (the "Software"), to deal
		in the Software without restriction, including without limitation the rights
		to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
		copies of the Software, and to permit persons to whom the Software is
		furnished to do so, subject to the following conditions:
		The above copyright notice and this permission notice shall be included in
		all copies or substantial portions of the Software.
		THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
		IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
		FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
		AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
		LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
		OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
		THE SOFTWARE.
		
		v1.1/		18/09/09 * bug fix by John V - http://blog.geekyjohn.com/
-------------------------------------------------*/

(function ($) {

    $.fn.quickPager = function (options) {

        var defaults = {
            pageSize: 10,
            currentPage: 1,
            holder: null,
            pagerLocation: "after",
            paginationSelector: $(this).children()
        };

        var options = $.extend(defaults, options);

        return this.each(function () {

            var selector = $(this);
            var pageCounter = 1;

            selector.find(options.paginationSelector).each(function (i) {

                if (i < pageCounter * options.pageSize && i >= (pageCounter - 1) * options.pageSize) {
                    $(this).addClass("simplePagerPage" + pageCounter);
                }
                else {
                    $(this).addClass("simplePagerPage" + (pageCounter + 1));
                    pageCounter++;
                }
            });

            // show/hide the appropriate regions 
            selector.find(options.paginationSelector).hide();
            selector.find(".simplePagerPage" + options.currentPage).show();

            if (pageCounter <= 1) {
                return;
            }

            //Build pager navigation
            var pageNav = "<div class='simplePagerNav'>";
            for (i = 1; i <= pageCounter; i++) {
                if (i == options.currentPage) {
                    pageNav += "<div class='currentPage simplePageNav" + i + "'><a rel='" + i + "' href='#'>" + i + "</a></div>";
                }
                else {
                    pageNav += "<div class='simplePageNav" + i + "'><a rel='" + i + "' href='#'>" + i + "</a></div>";
                }
            }
            pageNav += "</div>";

            if (!options.holder) {
                switch (options.pagerLocation) {
                    case "before":
                        selector.before(pageNav);
                        break;
                    case "both":
                        selector.before(pageNav);
                        selector.after(pageNav);
                        break;
                    default:
                        selector.after(pageNav);
                }
            }
            else {
                $(options.holder).append(pageNav);
            }

            //pager navigation behaviour
            selector.parent().find(".simplePagerNav a").click(function () {

                //grab the REL attribute 
                var clickedLink = $(this).attr("rel");
                options.currentPage = clickedLink;

                if (options.holder) {
                    $(this).parent("div").parent("div").parent(options.holder).find("div.currentPage").removeClass("currentPage");
                    $(this).parent("div").parent("div").parent(options.holder).find("a[rel='" + clickedLink + "']").parent("div").addClass("currentPage");
                }
                else {
                    //remove current current (!) page
                    $(this).parent("div").parent("div").parent(".simplePagerContainer").find("div.currentPage").removeClass("currentPage");
                    //Add current page highlighting
                    $(this).parent("div").parent("div").parent(".simplePagerContainer").find("a[rel='" + clickedLink + "']").parent("div").addClass("currentPage");
                }

                //hide and show relevant links
                selector.children().hide();
                selector.find(".simplePagerPage" + clickedLink).show();

                return false;
            });
        });
    }
})(jQuery);
