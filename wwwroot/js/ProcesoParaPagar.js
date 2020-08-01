 
    function ShowMap() {

        Swal.fire({
            title: "<strong> Selecione su hubicacion </strong>",
            html:
                "<div id='mapid' style='width:80%; height:300px; margin:auto;'> </div>",
            showConfirmButton: true,
            showCancelButton: true,
            confirmButtonText: 'Continuar con la compra',
            CancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.value) {
                Swal.fire({
                    title: 'Introdusca su contacto',
                    input: 'tel',
                    showCancelButton: true,
                    confirmButtonText: 'Continuar con la compra',
                    CancelButtonText: 'Cancelar'
                }).then((result) => {
                    Swal.fire({
                        title: 'Describa su direccion',
                        input: 'textarea',
                        inputPlaceholder: 'Escriba aqui su comentario...',
                        CancelButton: true,
                        CancelButtonText: 'Cancelar'
                    })
                }).then((result) => {
                    Swal.fire({
                        title: 'Realizar pago de la compra',
                        showCancelButton: true,
                        showConfirmButton: false,
                        cancelButtonText: 'Cancelar compra',
                        html: '<div id="paypal-button-container"></div>'
                    })
                    PaypalPay();
                })
            }

        })

            addMap();
        }


        function addMap() {
        // mostrar mapa leaftlet y optener coordenadas del usuario
        getLocation();

            function getLocation() {
                if (navigator.geolocation) {
        navigator.geolocation.watchPosition(showPosition);
                } else {
        alert("Geolocation no es soportada por su navegador");
                }
            }

            function showPosition(position) {
                let latitud = position.coords.latitude;
                let longitud = position.coords.longitude;

                var map = L.map('mapid').setView([latitud, longitud], 14);


                L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
        attribution: '',
                    maxZoom: 30,
                    id: 'mapbox/streets-v11',
                    tileSize: 512,
                    zoomOffset: -1,
                }).addTo(map);


                var popup = L.popup();

                function onMapClick(e) {
        popup
            .setLatLng(e.latlng)
            .setContent("Su posicion es: " + e.latlng.toString())
            .openOn(map);
                }

                map.on('click', onMapClick);
            }

    }


        function PaypalPay() {
            var precio = document.getElementById("precio").value;

        // Render the PayPal button into #paypal-button-container
        paypal.Buttons({

        style: {
        layout: 'horizontal'
            },
            // Set up the transaction
            createOrder: function (data, actions) {
                return actions.order.create({
        purchase_units: [{
        amount: {
        value: precio
                        }
                    }]
                });
            },

            // Finalize the transaction
            onApprove: function (data, actions) {
                return actions.order.capture().then(function (details) {
                    // Show a success message to the buyer
                    Swal.fire({
                        title: 'Transacion realizada',
                        icon: 'success',
                        text: 'Muchas gracias ' + details.payer.name.given_name
                     });
                });
            }
        }).render('#paypal-button-container');

    }
 