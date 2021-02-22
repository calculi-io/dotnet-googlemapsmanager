var locations = {};

(function ($) {
    var performZoom = false;
    locations.map = null;
    locations.bounds = null;
    locations.infoBubble = null;
    locations.markers = [];
    locations.gmarkers = [];
    locations.filters = [];
    locations.missedMarkers = [];

    locations.getMarkers = function () {
        var baseUrl = $("#BaseUrlAjax").data("value");
        $.ajax({
            url: baseUrl + "LocationMap/GetLocations",
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            xhrFields: {
                withCredentials: true
            },
            crossDomain: true,
            success: function (data) {
                locations.markers = [];
                data = data.Data;
                $(data).each(function () {
                    var record = [];
                    var html = "";
                    record.push($(this)[0].id);
                    record.push($(this)[0].Title);
                    record.push($(this)[0].Latitude);
                    record.push($(this)[0].Longitude);
                    html = '<div id="pin-details"><div class="title"><h1>' + $(this)[0].Title + '</h1></div>';
                    if ($(this)[0].PlantSummary)
                        html += '<div class="title">' + $(this)[0].PlantSummary + '</div>';

                    html += '<div class="address"><p>';

                    if ($(this)[0].Address != null) {
                        html += $(this)[0].Address;
                    }

                    html += '<br />';

                    if ($(this)[0].Locality != null) {
                        html += $(this)[0].Locality + ' ';
                    }

                    if ($(this)[0].PostalCode != null) {
                        html += $(this)[0].PostalCode;
                    }

                    html += '<br />';

                    if ($(this)[0].Country != null) {
                        html += $(this)[0].Country;
                    }
                    
                    if ($(this)[0].Telephone != null) {
                        html += '<br />' + $(this)[0].Telephone;
                    }
                    html += '</p></div>';

                    if ($(this)[0].ProductTypes && $(this)[0].ProductTypes != 'null') {
                        html += '<div class="categories"><h4>' + $(this)[0].ProductTypes + '</h4></div>';
                    }

                    html += '<div class="categories"><h4>' +
                        ($(this)[0].PlantManager ? 'Manager: ' + $(this)[0].PlantManager : '') +
                        ($(this)[0].Operations ? '<br />Operations: ' + $(this)[0].Operations: '') +
                        ($(this)[0].HR ? '<br />HR: ' + $(this)[0].HR: '') +
                        ($(this)[0].Sales ? '<br />Sales: ' + $(this)[0].Sales : '') + '</h4></div>';

                    html += '<a class="directions" href="http://maps.google.com/maps/dir/Current+Location/' + $(this)[0].Latitude + ',' + $(this)[0].Longitude + '" target="blank">Directions</a></div>';
                    record.push(html);

                    var region = [];
                    if ($(this)[0].Region) {
                        region = $(this)[0].Region.split(",");
                    }
                    record.push(region);

                    var facilityTypes = [];
                    if ($(this)[0].FacilityTypes) {
                        facilityTypes = $(this)[0].FacilityTypes.split(",");
                    }
                    record.push(facilityTypes);

                    var productTypes = [];
                    if ($(this)[0].ProductTypes) {
                        productTypes = $(this)[0].ProductTypes.split(",");
                    }
                    record.push(productTypes);

                    record.push($(this)[0].SitecoreId);
                    record.push($(this)[0].Title);

                    var textToPush = '<div class="address"><p>';

                    if ($(this)[0].Address != null) {
                        textToPush += $(this)[0].Address + '<br />';
                    }

                    if ($(this)[0].Locality != null) {
                        textToPush += $(this)[0].Locality + ' ';
                    }

                    if ($(this)[0].PostalCode != null) {
                        textToPush += $(this)[0].PostalCode;
                    }

                    textToPush += '<br />';

                    if ($(this)[0].Country != null) {
                        textToPush += $(this)[0].Country;
                    }

                    textToPush += '</p></div>';

                    record.push(textToPush);

                    var item_exp_info = {};
                    var item_info = $(this)[0];
                    item_exp_info.PlantId = item_info.PlantId;
                    item_exp_info.Address = item_info.Address;
                    item_exp_info.Suite = item_info.Suite;
                    item_exp_info.Locality = item_info.Locality;
                    item_exp_info.PostalCode = item_info.PostalCode;
                    item_exp_info.Country = item_info.Country;
                    item_exp_info.Telephone = item_info.Telephone;
                    item_exp_info.PlantManager = item_info.PlantManager;
                    item_exp_info.PlantManagerEmail = item_info.PlantManagerEmail;

                    record.push(item_exp_info);

                    locations.markers.push(record);
                });

                locations.markers.sort(function (a, b) {
                    var a_name = a[1].toUpperCase();
                    var b_name = b[1].toUpperCase();
                    if (a_name < b_name) {
                        return -1;
                    }
                    if (a_name > b_name) {
                        return 1;
                    }
                    return 0;
                });

                locations.initialize(locations.markers);
                $('.map-wrapper').removeClass("hidden");
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
            }
        });
    }

    function openMap() {
        locations.infoBubble.close();
        $('.map-wrapper').addClass('open');
    }

    function closeMap() {
        $('#perspective').removeClass('map-open');
        $('.map-wrapper').removeClass('open');
    }

    // If a specific location is passed the map will center and pop the detail card
    function gotoSelected(location) {
        var len = locations.markers.length;
        for (var i = 0; i < len; i++) {
            if (location === locations.markers[i][8]) {
                locations.setSelectedMarker(i);
            }
        }
    }

    locations.zoomControl = function ZoomControl(controlDiv, map) {
        // Creating divs & styles for custom zoom control
        controlDiv.style.padding = '30px';

        // Set CSS for the control wrapper
        var controlWrapper = document.createElement('div');
        controlWrapper.className = 'control-wrapper';
        controlDiv.appendChild(controlWrapper);

        // Set CSS for the zoomIn
        var zoomInButton = document.createElement('div');
        zoomInButton.className = 'zoom in';
        controlWrapper.appendChild(zoomInButton);

        // Set CSS for the zoomOut
        var zoomOutButton = document.createElement('div');
        zoomOutButton.className = 'zoom out';
        controlWrapper.appendChild(zoomOutButton);

        // Setup the click event listener - zoomIn
        google.maps.event.addDomListener(zoomInButton, 'click', function () {
            map.setZoom(map.getZoom() + 1);
        });

        // Setup the click event listener - zoomOut
        google.maps.event.addDomListener(zoomOutButton, 'click', function () {
            map.setZoom(map.getZoom() - 1);
        });

    };

    locations.addMarker = function (marker) {
        var title = marker[1];
        var pos = new google.maps.LatLng(marker[2], marker[3]);
        var content = marker[4];

        var region = marker[5];
        var productCategory = marker[6];
        var type = marker[7];
        var locality = [];
        locality.push(marker[9]);

        var categories = [region, productCategory, type, locality];

        //var markerIcon = new google.maps.MarkerImage("../content/images/marker-icon.png", null, null, null, new google.maps.Size(36, 50));
        //var markerHoverIcon = new google.maps.MarkerImage("../content/images/marker-icon-hover.png", null, null, null, new google.maps.Size(36, 50));
        var markerIcon = new google.maps.MarkerImage("content/images/marker-icon.png", null, null, null, new google.maps.Size(36, 50));
        var markerHoverIcon = new google.maps.MarkerImage("content/images/marker-icon-hover.png", null, null, null, new google.maps.Size(36, 50));

        var gmarker = new google.maps.Marker({
            title: title,
            content: content,
            position: pos,
            category: categories,
            map: map,
            icon: markerIcon
        });

        google.maps.event.addListener(gmarker, 'mouseover', function () {
            gmarker.setIcon(markerHoverIcon);
        });
        google.maps.event.addListener(gmarker, 'mouseout', function () {
            gmarker.setIcon(markerIcon);
        });

        locations.gmarkers.push(gmarker);

        // Marker click listener
        google.maps.event.addListener(gmarker, 'click', (function (gmarker, content) {
            return function () {
                locations.infoBubble.setContent(content);
                locations.infoBubble.open(map, gmarker);
                locations.map.panTo(this.getPosition());
                if (performZoom) {
                    locations.map.setZoom(5);
                    performZoom = false;
                }
                // set dropshadow style on infoBubble
                setTimeout(function () {
                    var infoBubbleContainer = document.getElementById('pin-details');
                    var infoBubbleParent = infoBubbleContainer.parentNode;
                    var infoBubbleGrandparent = infoBubbleParent.parentNode;
                    infoBubbleGrandparent.style['box-shadow'] = '0 0 10px 0 rgba(0,0,0,0.25)';
                    infoBubbleGrandparent.style['overflow-x'] = 'hidden';
                }, 500);
            };
        })(gmarker, content));

        google.maps.event.addListener(locations.map, "click", function (event) {
            locations.infoBubble.close();
        });
    };

    locations.countFilters = function (filters, filterCount, totalCount) {
        if (filterCount > 0) {
            //show filter count
            filters.prev().find('span').html(' (' + filterCount + ' of ' + totalCount + ')');
            filters.find('.clear-filters').removeClass('hide');
        } else {
            //clear filter count
            filters.prev().find('span').html('');
            filters.find('.clear-filters').addClass('hide');
        }
    };

    //handle sidebar updating based on visible markers
    locations.updateSidebar = function () {
        var html = "";
        var count = 0;
        var len = locations.markers.length;
        var bounds = locations.map.getBounds();

        for (var i = 0; i < len; i++) {
            if (locations.gmarkers[i].getVisible() && bounds.contains(locations.gmarkers[i].getPosition())) {
                html += '<a href="javascript:locations.setSelectedMarker(' + i + ')"><div class="location-details"><div class="title">' + locations.markers[i][9] + '</div>' + locations.markers[i][10] + '</div><\/a><br>';
                count++;
            }
        }
        //update sidebar HTML based on zoom level
        $('#sidebar-container').html(html);
        if (count === 1) {
            $('#location-count').html('Showing ' + count + ' Location');
        } else {
            $('#location-count').html('Showing ' + count + ' Locations');
        }
    };

    locations.downloadSelectedLocations = function () {
        var csv_content = "";
        var count = 0;
        var len = locations.markers.length;
        var bounds = locations.map.getBounds();
                
        for (var i = 0; i < len; i++) {
            if (locations.gmarkers[i].getVisible() && bounds.contains(locations.gmarkers[i].getPosition())) {
                var marker = locations.markers[i];

                var item_exp_info = marker[11];
                var item = [
                    '"' + (item_exp_info.PlantId ? item_exp_info.PlantId.toString() : '') + '"',
                    '"' + (marker[1] ? marker[1] : '') + '"',
                    '"' + (item_exp_info.Address ? item_exp_info.Address : '') + '"',
                    '"' + (item_exp_info.Suite ? item_exp_info.Suite : '') + '"',
                    '"' + (item_exp_info.Locality ? item_exp_info.Locality : '') + '"',
                    '"' + (item_exp_info.PostalCode ? item_exp_info.PostalCode : '') + '"',
                    '"' + (item_exp_info.Country ? item_exp_info.Country : '') + '"',
                    '"' + (item_exp_info.Telephone ? item_exp_info.Telephone : '') + '"',
                    '"' + (marker[5] ? marker[5].join(',') : '') + '"',
                    '"' + (marker[7] ? marker[7].join(',') : '') + '"',
                    '"' + (marker[6] ? marker[6].join(',') : '') + '"',
                    '"' + (item_exp_info.PlantManager ? item_exp_info.PlantManager : '') + '"',
                    '"' + (item_exp_info.PlantManagerEmail ? item_exp_info.PlantManagerEmail : '') + '"'
                ]

                csv_content += ((count > 0 ? '\n' : '') + item.join(','));
                count++;
            }
        }
        var file_header = ['"Plant #"',
            '"Title"',
            '"Address"',
            '"Suite"',
            '"Locality"',
            '"Postal Code"',
            '"Country"',
            '"Telephone"',
            '"Region"',
            '"Product Types"',
            '"Facility Types"',
            '"Plant Manager"',
            '"Plant Manager Email"'];
        var file_header_str = file_header.join(',') + '\n';

        if (count > 0) {
            var file_name = 'Google Map Selected Locations.csv';
            if (window.navigator.msSaveOrOpenBlob && window.Blob) {
                csv_content = file_header_str + csv_content;
                var blob = new Blob([csv_content], { type: "text/csv" });
                navigator.msSaveOrOpenBlob(blob, file_name);
            } else {
                csv_content = 'data:text/csv;charset=utf-8,' + file_header_str + csv_content;
                var encodedUri = encodeURI(csv_content);
                var link = document.createElement("a");
                link.setAttribute("href", encodedUri);
                link.setAttribute("download", file_name);
                document.body.appendChild(link);
                link.click();
            }
        }
    };

    // Trigger a click action based on external interaction (sidebar, query string)
    locations.setSelectedMarker = function (i) {
        performZoom = true;
        google.maps.event.trigger(locations.gmarkers[i], "click");
        //setTimeout(function(){ // Wait for map to initialize, then zoom to location
        //  locations.map.setZoom(4);
        //}, 250);
    };

    locations.showAllMarkers = function () {
        //show all markers
        for (var k = 0; k < locations.markers.length; k++) {
            locations.gmarkers[k].setVisible(true);

            //zoom to selection... need to animate somehow
            //locations.bounds.extend(locations.gmarkers[k].position);
            //locations.map.fitBounds(locations.bounds);
        }

        //enable zoom limits
        locations.map.zoomLimits = true;

        locations.map.fitBounds(locations.worldBounds);

        //clear filter count
        $('.map-filters').each(function () {
            locations.countFilters($(this), 0, 0);
        });

        $('#map-container').removeClass('no-matches');
        $('#no-match-message').removeClass('visible');
    };

    locations.filterMarkers = function () {
        locations.filters = [];
        var i = 0;
        var filterCount = 0;
        var totalCount = 0;


        $('.map-filters').each(function () {
            locations.filters.push([]);

            $(this).find('.map-filter').each(function () {
                if ($(this).is(':checked')) {
                    locations.filters[i].push($(this).val());
                    filterCount++;
                }
                totalCount++;
            });

            locations.countFilters($(this), filterCount, totalCount);

            i++;
            filterCount = 0;
            totalCount = 0;
        });

        if ($('.map-filter:checked').length > 0) {
            locations.revealFilteredMarkers(locations.filters);
            $('.clear-all-filters').removeClass('hide');
        } else {
            locations.showAllMarkers();
            $('.clear-all-filters').addClass('hide');
        }
    };

    locations.clearAllFilters = function () {
        $('.map-filter').prop('checked', false);
        $('.clear-all-filters').addClass('hide');
        $('input .localitySearch').val('');

        locations.showAllMarkers();
    };

    locations.clearFilterCategory = function (buttonClicked) {
        buttonClicked.parent().find($('.map-filter')).prop('checked', false);
        buttonClicked.addClass('hide');
        locations.filterMarkers();
    };

    locations.revealFilteredMarkers = function (filters) {
        var matchingCategories;
        var matchingFilters;
        var visibleMarkers = 0;
        locations.bounds = new google.maps.LatLngBounds();
        locations.infoBubble.close();

        //check each marker
        for (var j = 0; j < locations.markers.length; j++) {
            var marker = locations.gmarkers[j];
            var visible = false;
            matchingCategories = 0;

            //check each filter type with selections
            for (var filterType = 0; filterType < filters.length; filterType++) {
                //check if any filters are selected in filter type
                if (filters[filterType].length > 0) {
                    matchingFilters = 0;

                    //check each filter selected
                    for (var filterSelection = 0; filterSelection < filters[filterType].length; filterSelection++) {

                        //check each category group on the marker
                        for (var categoryGroup = 0; categoryGroup < marker.category.length; categoryGroup++) {

                            //check each category on the marker
                            for (var markerCategory = 0; markerCategory < marker.category[categoryGroup].length; markerCategory++) {
                                if (marker.category[categoryGroup][markerCategory] == filters[filterType][filterSelection]) { //check to see if any filters selected
                                    matchingFilters++;
                                }
                                if (marker.category[categoryGroup][markerCategory].toLowerCase().indexOf(filters[filterType][filterSelection].toLowerCase()) > -1) {
                                    matchingFilters++;
                                }
                            }
                        }
                    }

                    //if there are any matching filters, add to matching categories
                    if (matchingFilters > 0) {
                        matchingCategories++;
                    }

                } else {
                    //add to matching categories if no selections made
                    matchingCategories++;
                }
            }

            //check if matching categories match amount of filter dropdowns on the page
            if (matchingCategories == filterType) {
                visible = true;
                visibleMarkers++;
                locations.bounds.extend(marker.position);
            }
            marker.setVisible(visible);
        }

        //enable zoom limits
        locations.map.zoomLimits = true;

        if (visibleMarkers > 0) {
            //zoom to selection
            locations.map.fitBounds(locations.bounds);
            $('#map-container').removeClass('no-matches');
            $('#no-match-message').removeClass('visible');
        } else {
            $('#map-container').addClass('no-matches');
            $('#no-match-message').addClass('visible');
        }
    };

    locations.initialize = function (markers) {
        //google maps parameters
        var latitude = 33.300888,
            longitude = -81.71991589999999,
            map_zoom = 12;

        //basic color of the map, plus a value for saturation and brightness
        var main_color = '#2d313f',
            saturation_value = -20,
            brightness_value = 5;

        //map style definitions
        var style = [
            {
                //set saturation for the labels on the map
                elementType: "labels",
                stylers: [
                    { saturation: saturation_value }
                ]
            },
            {	//poi stands for point of interest - don't show these lables on the map
                featureType: "poi",
                elementType: "labels",
                stylers: [
                    { visibility: "off" }
                ]
            },
            {
                //don't show highways lables on the map
                featureType: 'road.highway',
                elementType: 'labels',
                stylers: [
                    { visibility: "off" }
                ]
            },
            {
                //don't show local road lables on the map
                featureType: "road.local",
                elementType: "labels.icon",
                stylers: [
                    { visibility: "off" }
                ]
            },
            {
                //don't show arterial road lables on the map
                featureType: "road.arterial",
                elementType: "labels.icon",
                stylers: [
                    { visibility: "off" }
                ]
            },
            {
                //don't show road lables on the map
                featureType: "road",
                elementType: "geometry.stroke",
                stylers: [
                    { visibility: "off" }
                ]
            },
            //style different elements on the map
            {
                featureType: "transit",
                elementType: "geometry.fill",
                stylers: [
                    { hue: main_color },
                    { visibility: "on" },
                    { lightness: brightness_value },
                    { saturation: saturation_value }
                ]
            },
            {
                featureType: "poi",
                elementType: "geometry.fill",
                stylers: [
                    { hue: main_color },
                    { visibility: "on" },
                    { lightness: brightness_value },
                    { saturation: saturation_value }
                ]
            },
            {
                featureType: "poi.government",
                elementType: "geometry.fill",
                stylers: [
                    { hue: main_color },
                    { visibility: "on" },
                    { lightness: brightness_value },
                    { saturation: saturation_value }
                ]
            },
            {
                featureType: "poi.sport_complex",
                elementType: "geometry.fill",
                stylers: [
                    { hue: main_color },
                    { visibility: "on" },
                    { lightness: brightness_value },
                    { saturation: saturation_value }
                ]
            },
            {
                featureType: "poi.attraction",
                elementType: "geometry.fill",
                stylers: [
                    { hue: main_color },
                    { visibility: "on" },
                    { lightness: brightness_value },
                    { saturation: saturation_value }
                ]
            },
            {
                featureType: "poi.business",
                elementType: "geometry.fill",
                stylers: [
                    { hue: main_color },
                    { visibility: "on" },
                    { lightness: brightness_value },
                    { saturation: saturation_value }
                ]
            },
            {
                featureType: "transit",
                elementType: "geometry.fill",
                stylers: [
                    { hue: main_color },
                    { visibility: "on" },
                    { lightness: brightness_value },
                    { saturation: saturation_value }
                ]
            },
            {
                featureType: "transit.station",
                elementType: "geometry.fill",
                stylers: [
                    { hue: main_color },
                    { visibility: "on" },
                    { lightness: brightness_value },
                    { saturation: saturation_value }
                ]
            },
            {
                featureType: "landscape",
                stylers: [
                    { hue: main_color },
                    { visibility: "on" },
                    { lightness: brightness_value },
                    { saturation: saturation_value }
                ]

            },
            {
                featureType: "road",
                elementType: "geometry.fill",
                stylers: [
                    { hue: main_color },
                    { visibility: "on" },
                    { lightness: brightness_value },
                    { saturation: saturation_value }
                ]
            },
            {
                featureType: "road.highway",
                elementType: "geometry.fill",
                stylers: [
                    { hue: main_color },
                    { visibility: "on" },
                    { lightness: brightness_value },
                    { saturation: saturation_value }
                ]
            },
            {
                featureType: "water",
                elementType: "geometry",
                stylers: [
                    { hue: main_color },
                    { visibility: "on" },
                    { lightness: brightness_value },
                    { saturation: saturation_value }
                ]
            }
        ];

        //google map options
        var mapOptions = {
            center: new google.maps.LatLng(latitude, longitude),
            zoom: map_zoom,
            maxZoom: 15,
            panControl: false,
            zoomControl: false,
            mapTypeControl: false,
            streetViewControl: false,
            scrollwheel: true,
            styles: style
        };

        locations.infoBubble = new InfoBubble({
            content: '',
            backgroundColor: 'white',
            borderRadius: 0,
            borderWidth: 0,
            hideCloseButton: true,
            maxWidth: 300,
            padding: 0,
            shadowStyle: 3
        });

        map = new google.maps.Map(document.getElementById('map-container'), mapOptions);
        locations.map = map;

        //set zoom controls
        var zoomControlDiv = document.createElement('div');
        var zoomControl = new locations.zoomControl(zoomControlDiv, map);

        zoomControlDiv.index = 1;
        map.controls[google.maps.ControlPosition.RIGHT_BOTTOM].push(zoomControlDiv);

        // initialize boundary
        locations.bounds = new google.maps.LatLngBounds();

        locations.worldBounds = new google.maps.LatLngBounds(
            new google.maps.LatLng(62, -150),           // top left corner of map
            new google.maps.LatLng(-38, 175)            // bottom right corner
        );

        locations.map.fitBounds(locations.worldBounds);

        //set fitbounds listener to set min zoom level
        google.maps.event.addListener(map, 'zoom_changed', function () {
            zoomChangeBoundsListener =
                google.maps.event.addListener(map, 'bounds_changed', function (event) {
                    if (this.getZoom() > 5 && this.zoomLimits === true) {
                        // Change max/min zoom here
                        this.setZoom(5);
                    }
                    this.zoomLimits = false;
                    google.maps.event.removeListener(zoomChangeBoundsListener);
                });

        });

        google.maps.event.addListener(map, 'idle', function (event) { // Only fires after map has stopped moving
            locations.updateSidebar();
        });

        //enable zoom limits for initial load
        locations.map.zoomLimits = true;

        //load markers
        for (i = 0; i < markers.length; i++) {
            locations.addMarker(locations.markers[i]);
        }

        //bind map open
        $(document).on('click touchend', '.open-map', function (e) {
            e.preventDefault();
            locations.clearAllFilters();
            locations.showAllMarkers();
            window.location.hash = 'map';
        });

        //bind map close
        $(document).on('click touchend', '.close-map', function (e) {
            e.preventDefault();
            history.pushState('', document.title, window.location.pathname);
            closeMap();
        });

        //bind filtering
        $(document).on('change', '.map-filter', function () {
            locations.filterMarkers();
        });

        //bind clear all filters
        $(document).on('click touchend', '.clear-all-filters', function (e) {
            e.preventDefault();
            locations.clearAllFilters();
        });

        //bind clear filter category
        $(document).on('click touchend', '.clear-filters', function (e) {
            e.preventDefault();
            locations.clearFilterCategory($(this));
        });

        //  searchbox
        $(document).on("keypress", '.localitySearch', function (e) {
            if (e.keyCode == '13') {
                e.preventDefault();
                $('.searchLocalityButton').click();
            }
        });

        $('#searchform').submit(function (e) {
            e.preventDefault();
        });

        $(document).on("click touchend", '.searchLocalityButton', function (e) {
            e.preventDefault();
            var filters = [[], [], []];
            var locality = [];
            locality.push($('.localitySearch').val());
            filters.push(locality);

            locations.revealFilteredMarkers(filters);
            $('.clear-all-filters').removeClass('hide');
            openMap();
        });

        $(document).on('click touchend', '.prefilter', function (e) {
            e.preventDefault();
            var prefilters = $(this).data('prefilters');

            locations.clearAllFilters();

            $.each(prefilters, function (index, prefilter) {
                $('#' + prefilter).prop('checked', true);
            });

            locations.filterMarkers();

            openMap();
        });

        // bind show/hide filters on mobile
        $(document).on('click touchend', '.show-map-filters', function (e) {
            if ($(this).hasClass('open')) {
                $('.filter-wrapper').removeClass('open');
                $(this).removeClass('open');
            } else {
                $('.filter-wrapper').addClass('open');
                $(this).addClass('open');
            }
        });

        //check for map hash
        $(window).on('load', function () {
            if (window.location.hash.substr(1) === 'map') {
                openMap();
            }

            //check for map location id
            if (window.location.search.substr(1)) {
                var tempLocation = window.location.search.substr(1);
                var regex = /\{([a-z0-9\-]+)\}/i;
                var location = regex.exec(tempLocation);
                gotoSelected(location[1].toLowerCase());
            }
        });

        $(window).on('hashchange', function () {
            if (window.location.hash.substr(1) === 'map') {
                openMap();
            } else {
                closeMap();
            }
        });
    };

})(jQuery);


