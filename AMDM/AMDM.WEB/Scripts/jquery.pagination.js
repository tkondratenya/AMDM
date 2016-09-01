(function ($) {
    $.fn.pagination = function (options) {

        var defaults = {
            pageSize: 10,
            container: 'div.div-songtable',
            selector: '.div-songtable-row',
            navContainer: '.paging-nav',
            sortTag: '#views-sort',
            currentPage: 1,
            sorting : 0
        };

        var options = $.extend(defaults, options);

        function draw() {
            var pageCounter = 1;
            $(options.container).find(options.selector).each(function (i) {
                if (i < pageCounter * options.pageSize && i >= (pageCounter - 1) * options.pageSize) {

                   // $(this).attr('class', options.selector);
                    $(this).addClass("page" + pageCounter);
                }
                else {
                   // $(this).attr('class', options.selector);
                    $(this).addClass("page" + (pageCounter + 1));
                    pageCounter++;
                }
            });
            // show/hide the appropriate regions 
            $(options.container).find(options.selector).hide();
            $(options.container).find(".page" + options.currentPage).show();

            if (pageCounter <= 1) {
                return;
            }
            // build page navigation
            var pagingNav = '';
            for (i = 1; i <= pageCounter; i++) {
                if (i == options.currentPage) {
                    pagingNav += "<a class='current-nav nav" + i + "' rel='" + i + "'>" + i + "</a>";
                } else {
                    pagingNav += "<a class='nav" + i + "' rel='" + i + "'>" + i + "</a>";
                }
            }
            $(options.navContainer).empty().append(pagingNav);

            //grab the REL attribute 
            $(options.navContainer).find('a').click(function () {
                var clickedLink = $(this).attr("rel");
                options.currentPage = clickedLink;

                $(this).parent().find('.current-nav').removeClass('current-nav');
                $(this).parent().find("a[rel='" + clickedLink + "']").addClass('current-nav');

                $(options.container).find(options.selector).hide();
                $(options.container).find(".page" + clickedLink).show();
            });
        }

        function resetPages() {
            $(options.container).find(options.selector).each(function (i) {
                $(this).attr("class", "div-songtable-row");
            });
            }

        return this.each(function () {

            draw();

            $('#pagin10').click(function () {
                options.currentPage = 1;
                options.pageSize = 10;
                resetPages();
                draw();
            });
            $('#pagin15').click(function () {
                options.currentPage = 1;
                options.pageSize = 15;
                resetPages();
                draw();
            });
            $('#pagin20').click(function () {
                options.currentPage = 1;
                options.pageSize = 20;
                resetPages();
                draw();
            });

            $(options.sortTag).click(function () {
                resetPages();
                var cl = $(this).attr("class");              
                var colcl = "div-songtable-col3";

                if (cl == "div-songtable-col3-default") {
                    $(this).attr("class", "div-songtable-col3-des");
                    $(this).find("div").attr("class", "arrow-down");
                    options.sorting = -1;
                }
                else if (cl == "div-songtable-col3-asc") {
                    $(this).attr("class", "div-songtable-col3-des");
                    $(this).find("div").attr("class", "arrow-down");
                    options.sorting = -1;
                }
                else if (cl == "div-songtable-col3-des") {
                    $(this).attr("class", "div-songtable-col3-asc");
                    $(this).find("div").attr("class", "arrow-up");
                    options.sorting = 1;
                }

                $(".div-songtable-row").detach().sort(function (a, b) {
                    var str1 = parseInt($(a).find('.' + colcl).text(), 10);
                    var str2 = parseInt($(b).find('.' + colcl).text(), 10);
                    if (options.sorting == 1) {
                        return str1 > str2 ? 1 : -1;
                    } else if (options.sorting == -1) {
                        return str1 > str2 ? -1 : 1;
                    }
                }).appendTo($(".div-songtable"));
                draw();
            });

        });
    }
})(jQuery);