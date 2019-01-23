$(document).ready(function ()
{
    {$ConditionStartStatement$}

        $('#displayModal').foundation('reveal', 'open', {
            ajax: true,
            url: '{PopupAction}',
            success: function (data) {

            },
        });

        $(document).on('opened', '[data-reveal]', function () {
            initMap();
        });
        { $ConditionEndStatement$ }

});
