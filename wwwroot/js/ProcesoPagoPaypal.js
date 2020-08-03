var popup,  lat, lon,  telefono, comentario;    


function StartCompra() {

  Swal.fire({
        title: 'Marque su ubicacion',
        html: 
            "<div id='map' style='width:80%;height:300px;' class ='mx-auto'></div>",
        showConfirmButton: true,
        showCancelButton: true,
          cancelButtonText: 'Cancelar',
          inputAttributes: {
              maxlength: 11
          },
        confirmButtonText: 'Continuar',
          preConfirm: () => {
            getTelefono(); 
        }
  });

    getLocation(); 
    showPosition();
}


function getTelefono() {
    Swal.fire({
        title: 'Introdusca su numero telefono',
        input: 'number',
        inputPlaceholder: 'Numero telefonico',
        showConfirmButton: true,
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Continuar',
        inputValidator: (value) => {
            if (!value) {
                return 'Debes llenar este campo!'
            }
        },

        preConfirm: (tel) => { 
            direccionComentario(tel);
        } 
    });

}



function direccionComentario(tel) {
     Swal.fire({
        title: 'Describa su direccion',
        input: 'textarea',
        inputPlaceholder: 'Escriba su comentario...',
        showConfirmButton: true,
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
         confirmButtonText: 'Continuar',
        inputValidator: (value) => {
            if (!value) {
                return 'Debes llenar este campo!'
            }
        },
         preConfirm: (comment) => {
             PaypalButton(tel, comment);
        }
     });

}


function PaypalButton(tel, comment) {
  
        Swal.fire({
            title: 'Realizar pago de la compra',
            html: 
                '<div id="paypal-button-container"></div>',
            showConfirmButton:false,
            showCancelButton: true,
            cancelButtonText: 'Cancelar',         
        });

    // load paypal payment button 
       PaypalPay();

    // set tel and comment 
    telefono = tel;
    comentario = comment;
  
}

 


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

    var map = L.map('map').setView([latitud, longitud], 14);

    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
        attribution: '',
        maxZoom: 30,
        id: 'mapbox/streets-v11',
        tileSize: 512,
        zoomOffset: -1,
    }).addTo(map);

    L.marker([latitud, longitud]).addTo(map)

    map.on('click', onMapClick);

    function onMapClick(e) {

        let latlng = map.mouseEventToLatLng(e.originalEvent);
        var popup = L.popup();
        popup
            .setLatLng(e.latlng)
            .setContent("Nueva " + e.latlng.toString())
            .openOn(map);

        lat = latlng.lat ;
        lon = latlng.lng; 
        console.log(latlng.lat, latlng.lng)
   
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
                    text: 'Muchas gracias ' + details.payer.name.given_name,
                    showConfirmButton: true,
                    confirmButtonText: 'Finalizar compra',
                    preConfirm: () => {
                        conpraAprovada(); 
                    }
                });
            });
        }
    }).render('#paypal-button-container');

}


function compraAprovada() {
    var id = model.Id; //if you want to pass an Id parameter
    window.location.href = '@Url.Action("Comprado", "Home")/' + id;
} 
