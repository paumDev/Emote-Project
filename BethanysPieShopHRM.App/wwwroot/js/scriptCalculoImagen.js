function initializeImageInfo() {
    document.getElementById('inputImagen').addEventListener('change', function (event) {
        var file = event.target.files[0];
        if (file) {
            var infoDiv = document.getElementById('imagenInfo');
            var reader = new FileReader();

            reader.onload = function (e) {
                var img = new Image();
                img.src = e.target.result;

                img.onload = function () {
                    var ancho = img.naturalWidth;
                    var alto = img.naturalHeight;
                    var tamañoEnBytes = file.size;
                    var tamañoEnKB = (tamañoEnBytes / 1024).toFixed(2);
                    var tamañoEnMB = (tamañoEnBytes / (1024 * 1024)).toFixed(2);

                    infoDiv.innerHTML = `Dimensiones: ${ancho} x ${alto} px <br>
                                         Tamaño: ${tamañoEnKB} KB (${tamañoEnMB} MB)`;

                    // Llama al método .NET desde JavaScript para actualizar la base de datos
                    DotNet.invokeMethodAsync('YourBlazorAppNamespace', 'UpdateImageInfoInDatabase', ancho, alto, tamañoEnKB, tamañoEnMB);
                };
            };
            reader.readAsDataURL(file);
        } else {
            var infoDiv = document.getElementById('imagenInfo');
            infoDiv.innerHTML = 'No se ha seleccionado ninguna imagen.';
        }
    });
}
