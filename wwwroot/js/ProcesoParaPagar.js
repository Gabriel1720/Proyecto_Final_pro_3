 // mostrar el ontenedor con el mapa 
    function ShowMap() {
        // muestra el mapa, latitud y longitud 
        Swal.fire({
            title: "<strong> Selecione su hubicacion </strong>",
            html:
                "<div id='mapid' style='width:80%; height:300px; margin:auto;'> </div>",
            showConfirmButton: true,
            showCancelButton: true,
            confirmButtonText: 'Continuar con la compra',
            CancelButtonText: 'Cancelar'
         }).then((result) => {
       // pide el telefono del usuario 
                Swal.fire({
                    title: 'Introdusca su contacto',
                    input: 'number',
                    showCancelButton: true,
                    confirmButtonText: 'Continuar con la compra',
                    CancelButtonText: 'Cancelar'

                }).then((result) => {
                    // pide la descripcion de la direcion del cliente 
                     Swal.fire({
                        title: 'Describa su direccion',
                        input: 'textarea',
                        inputPlaceholder: 'Escriba aqui su comentario...',
                        CancelButton: true,
                        CancelButtonText: 'Cancelar'
                    }).then((result) => {
                        // llama el button de paypal para comenzar el proceso de pago
                        Swal.fire({
                            title: 'Realizar pago de la compra',
                            showCancelButton: true,
                            showConfirmButton: false,
                            cancelButtonText: 'Cancelar compra',
                            html: '<div id="paypal-button-container"></div>'
                        }); 
                        // cargar paypal process 
                        PaypalPay();
                    })

                }) 
     
        })
           // load mapa en el contanedor con la id map
            addMap();
        }


        function addMap() {
       
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

                         L.marker([latitud, longitud]).addTo(map)
 
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
 
/*
 *Pagar atravez de payapal, se requiere una cuenta de paypal para continuar con el pago.
 *El monto se debe pasar al campo value, para establecer el monto a pagar 
 * */

        function PaypalPay() {
             var precio = document.getElementById("total").value;

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
 