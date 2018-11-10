// process the form
$('form').submit(function (event) {

    var formData = {
        'startTime': $('input[id=startTime]').val(),
        'bedTime': $('input[id=bedTime]').val(),
        'endTime': $('input[id=endTime]').val()
    };

    // process the form
    $.ajax({
        type: 'GET', 
        url: 'api/IncomeCalculation', 
        data: formData, 
        dataType: 'json', 
        encode: true
    })
    .done(function (data) {
        $('input[id=income]').val(data.income.result);
    });

    // stop the form from submitting the normal way and refreshing the page
    event.preventDefault();
});