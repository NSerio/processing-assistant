/// <reference path="processingSet[index]-v1.0.js" />
(function (nm, $) {
    nm.init = function () {

        $.ajaxSetup({
            cache: false,
            error: function (xhr) {
                $('#dvIndex').html(xhr.responseText);
                $('#modal').removeClass('loading');
                $('#modalPopupLoading').removeClass('loading');
                $('#selectSourcePathModal').modal('hide');
            }
        });

        //All Functions!
        //----------------------------------------------------------------------------------
        nm.GetFolderTree = function () {
            var selectedValue = $('#folderList').val();
            $('#GetSubFolders').treeview({
                url: nm.URLGetFolders,
                ajax: {
                    type: 'post',
                    data: { 'sourcePath': selectedValue }
                },
                toggle: function () {
                    nm.HighlightSelected();
                }
            });
        };

        nm.HighlightSelected = $('#GetSubFolders').click(function onTreeViewClick(sender) {
            var clickedElement = $(sender.target);
            if (clickedElement.hasClass('hover')) {
                //Find all selected nodes and deselect them.
                var treeView = $(document.getElementById('GetSubFolders'));
                treeView.find(".selectedNode").removeClass('selectedNode');
                //Select newly selected node
                clickedElement.addClass('selectedNode');

                var parents = clickedElement.parent('li');
                var selectedTreeNodeId = parents[0].id;
                $("#selectedFolder").val(selectedTreeNodeId);
            }
        });

        //All Events!
        //----------------------------------------------------------------------------------

        $(document).ready(function () {
            nm.GetFolderTree();
            var selectedSourceLocation = $("#folderList").val();
            $("#selectedFolder").val(selectedSourceLocation);
        });

    };

})(window.ProcessingSetFolderBrowser = window.ProcessingSetFolderBrowser || {}, jQuery);

$(document).ajaxStart(function () {
    $('#modalPopupLoading').addClass('loading');
}).ajaxStop(function () {
    $('#modalPopupLoading').removeClass('loading');
});

