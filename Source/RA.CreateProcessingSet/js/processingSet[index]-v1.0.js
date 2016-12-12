(function (nm, $) {
    nm.init = function () {

        $.ajaxSetup({
            cache: false,
            error: function (xhr) {
                $('#dvIndex').html(xhr.responseText);
                $('#modal').removeClass('loading');
            }
        });

        $('input').placeholder();
        $('textarea').placeholder();

        //All Functions!
        //----------------------------------------------------------------------------------
        nm.ChangeFolder = function () {
            var selectedValue = $('#folderList').val();
            $.get(nm.URLBrowse,
                { sourcePath: selectedValue },
                function (data) {
                    $('#dvProcessingSourceList').html(data);
                }, 'html');
        };
        
        nm.GetCustodianSummary = function () {
            var $custodianSummary = $('#CustodianSummary');
            var level = $('#CustodianLevel').val();
            var path = $("#selectedFolder").val();
            var destination = $('input[name=Destination]:checked').val();
            $custodianSummary.find('ul').empty();
            if (level.trim() != '' && !isNaN(level) && path) {
                $.post(nm.URLGetCustodianSummary,
                    { path: path, level: level, destination: destination },
                    function (data) {
                        $custodianSummary.find('ul').empty();
                        $.each(data, function (i, el) {
                            var spanName = $('<strong>').text(el.CustodianNames.FullName);
                            var spanDescription = $('<span>').text(': ' + el.Description);
                            $custodianSummary
                                .find('ul')
                                .append($('<li>').append(spanName).append(spanDescription));
                        });
                    });
                $custodianSummary.show();
            } else {
                $custodianSummary.hide();
                $custodianSummary.find('ul').empty();
            }
        };
        //All Events!
        //----------------------------------------------------------------------------------
        $("#btnCreate").on('click', function () {
            $('#frmIndex').validate();

            if ($('#frmIndex').valid()) {
                $('#btnCreate').button('loading');
                $('#modal').addClass('loading');
            }
        });

        $("#btnBrowse").click(function () {
            $('#modal').addClass('loading');
            $.get(nm.URLBrowse, {}, function (data) {
                $('#dvProcessingSourceList').html(data);
                $('#selectSourcePathModal').modal('show');
                $('#modal').removeClass('loading');
            }, 'html');
        });

        $("#btnOK").on('click', function () {
            var selectedSourcePath = $("#selectedFolder").val();
          
            if (selectedSourcePath.length < 1) {
                $('#dvNoSelectedSourcePath').html("Please select a source path.");
            }
            else {
                $('#modal').addClass('loading');
                $('#dvNoSelectedSourcePath').html("");
                $('#txtSourcePath').val(selectedSourcePath);
                $('#txtSourcePath').text(selectedSourcePath);
                $('#selectSourcePathModal').modal('hide');
                $('#modal').removeClass('loading');
            }
            nm.GetCustodianSummary();
        });

        $('#CustodianLevel').keyup(function () {
            nm.GetCustodianSummary();
        });
        $('input[name=Destination]').change(function () {
            nm.GetCustodianSummary();
        });
        $('#folderList').change(function () {
            nm.ChangeFolder();
        });

    };

})(window.ProcessingSetIndex = window.ProcessingSetIndex || {}, jQuery);
