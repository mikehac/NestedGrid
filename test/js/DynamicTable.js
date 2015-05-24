function DynamicTable(tableObj) {

    var init = function (tblObj) {
        $.ajax('GetNestedGridData.ashx', {
            dataType: 'json',
            data: { rt: 'GetParrents' },
            success: function (resp, status, req) {
                var tr = '';
                $.each(resp, function (index, item) {
                    var img = '<img id="img_' + (index + 1).toString() + '" src="/images/plus.jpg" />';
                    tr += '<tr><td>' + img + item.PropertyName + '</td><td></td><td></td><td></td><td>' + item.Total + '</td></tr>';
                });

                $(tblObj).append(tr);
                $(tblObj + ' td img').unbind('click').bind('click', function () {
                    var parentID = $(this).attr('id').split('_')[1];
                    var detailRowsSelector = ".detailRows_" + parentID;
                    if ($(this).attr('src').indexOf('plus') > 0) {
                        $(this).attr('src', '/images/minus.jpg');
                        var selectedElement = $(this);
                        if ($(detailRowsSelector).length === 0) {
                            $.ajax('GetNestedGridData.ashx', {
                                dataType: 'json',
                                data: { rt: 'GetChildren', ParrentId: parentID },
                                success: function (resp, status, req) {
                                    var innerTR = '';
                                    $.each(resp, function (index, item) {
                                        innerTR += '<tr class="detailRows_' + parentID + '"><td></td><td>' + item.Name + '</td><td></td><td></td><td>' + item.Total.toString() + '</td></tr>';
                                    });

                                    selectedElement.parent().parent().after(innerTR);
                                }
                            });
                        }
                        else {
                            $(detailRowsSelector).show();
                        }
                    }
                    else {
                        $(this).attr('src', '/images/plus.jpg');
                        $(detailRowsSelector).hide();
                    }
                });
            }
        });
    };
    init(tableObj);
    var retObj = {
        Columns: []
    };

    return retObj;
}