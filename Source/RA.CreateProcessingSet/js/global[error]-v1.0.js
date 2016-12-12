(function (nm, $) {
    nm.init = function () {
        //all functions
        //----------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------

        //All Events!
        //----------------------------------------------------------------------------------
        $("#btnReturn").click(function (e) {
            e.preventDefault();
            $('#modal').addClass('loading');
            window.location.replace(nm.URLHomeIndex);
        });

        //----------------------------------------------------------------------------------
    };
})(window.ErrorIndex = window.ErrorIndex || {}, jQuery);