var request = request || {};

(function ($) {
    $(load);

    function load() {
       
    }

    request.hotelList_success = function (id) {
        //var now = new Date();
        //var fomattedNow = new String(now.toUTCString());
        /*
        $("#hotels :checkbox").change(function () {
            $.post("/Hotels/ChangeHotel",
                {
                    //"HotelId": $(this).data("id"),
                    //"IsCompleted": this.checked,
                    //"UpdatedOn": fomattedNow
                }
            );
        });
        */
        $("#hotelID").text("ZZZZ");
    };  

    request.initSuccess = function (id) {
        console.log("initSuccess");
    };

    

})(jQuery);
