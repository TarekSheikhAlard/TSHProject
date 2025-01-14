
var isMarker = false;
var poly;

initMap = function (id, lat, lng) {

    var $latInput = $('#XCoordinate'), $lngInput = $('#YCoordinate');
    var Coordinates, zoom;


    if (!isNaN(lat) && !isNaN(lng)) {

        Coordinates = { lat: parseFloat(lat), lng: parseFloat(lng) };
        zoom = 12;
    } else {
        Coordinates = { lat: 24.68, lng: 46.65 };
        zoom = 12
    }

    var map = new google.maps.Map(

        document.getElementById(id), {
        center: Coordinates,
        zoom: zoom,
        mapTypeId: google.maps.MapTypeId.ROADMAP

    });

    poly = new google.maps.Polyline({
        strokeColor: '#000000',
        strokeOpacity: 1.0,
        strokeWeight: 3
    });

    poly.setMap(map);


    return map;

}

CreateMarker = function (map, lat, lng, draggable, iconUrl, $Input) {
    var Coordinates;
    if (!isNaN(lat) && !isNaN(lng)) {

        Coordinates = { lat: parseFloat(lat), lng: parseFloat(lng) };

    }
    var icon = {
        url: iconUrl, // url
        scaledSize: new google.maps.Size(50, 50), // scaled size
        //origin: new google.maps.Point(0, 0), // origin
        anchor: new google.maps.Point(30, 30) // anchor
    };

    var marker = new google.maps.Marker({
        position: Coordinates,
        map: map,
        icon: icon,
        draggable: draggable
    });

    if (draggable) {
        google.maps.event.addListener(map, 'click', function (e) {

            console.log("Latitude: " + e.latLng.lat() + " " + ", longitude: " + e.latLng.lng());
            marker.setPosition(e.latLng);

            if ($Input.length > 0) {
                $Input.val(e.latLng.lat() + ',' + e.latLng.lng());

                //$latInput.val(e.latLng.lat())
                //$lngInput.val(e.latLng.lng())
            }




        });

        google.maps.event.addListener(marker, 'dragend', function (e) {

            if ($Input.length > 0) {
                $Input.val(e.latLng.lat() + ',' + e.latLng.lng());

                //$latInput.val(e.latLng.lat())
                //$lngInput.val(e.latLng.lng())
            }
            console.log("Latitude: " + e.latLng.lat() + " " + ", longitude: " + e.latLng.lng());
        });
    }
}

CreatePloyline = function (map, startX, StartY, EndX, EndY) {
    Coordinates = []
    Coordinates.push({ lat: parseFloat(startX), lng: parseFloat(StartY) });
    Coordinates.push({ lat: parseFloat(EndX), lng: parseFloat(EndY) });

    var flightPath = new google.maps.Polyline({
        path: Coordinates,
        geodesic: true,
        strokeColor: '#FF0000',
        strokeOpacity: 1.0,
        strokeWeight: 2
    });

    flightPath.setMap(map)
}