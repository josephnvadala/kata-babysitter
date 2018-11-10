// process the form
$('form').submit(function (event) {

    // get the form data
    // there are many ways to get this data using jQuery (you can use the class or id also)
    var formData = {
        'startTime': $('input[id=startTime]').val(),
        'bedTime': $('input[id=bedTime]').val(),
        'endTime': $('input[id=endTime]').val()
    };

    // process the form
    $.ajax({
        type: 'POST', // define the type of HTTP verb we want to use (POST for our form)
        url: 'api/IncomeCalculation', // the url where we want to POST
        data: formData, // our data object
        dataType: 'json', // what type of data do we expect back from the server
        encode: true
    })
        // using the done promise callback
        .done(function (data) {

            // log data to the console so we can see
            console.log(data);

            // here we will handle errors and validation messages
        });

    // stop the form from submitting the normal way and refreshing the page
    event.preventDefault();
});

function submitTimes(startTime, endTime, bedTime) {
    //$.ajax({
    //    type: 'POST',
    //    url: 'api/IncomeCalculation',
    //    data: JSON.stringify({
    //        StartTime: startTime,
    //        EndTime: endTime,
    //        BedTime: bedTime
    //    }),
    //    contentType: 'application/json; charset=utf-8',
    //    dataType: 'json',
    //    success: function (msg) {
    //        // Do something interesting here.
    //        alert("It worked!");
    //    }
    //});
}



const makeRequest = async () => {
    try {
        // this parse may fail
        const data = JSON.parse(await getJSON())
        console.log(data)
    } catch (err) {
        console.log(err)
    }
}