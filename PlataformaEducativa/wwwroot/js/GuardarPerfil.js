function mostrarImagen() {
    var inputFile = document.getElementById('inputFile');
    var previewImage = document.getElementById('previewImage');

    // Verificar si se seleccionó un archivo
    if (inputFile.files.length > 0) {
        var selectedFile = inputFile.files[0];

        // Verificar si el archivo es una imagen
        if (validarTipoImagen(selectedFile.type)) {
            // Crear un objeto URL para mostrar la imagen
            var imageUrl = URL.createObjectURL(selectedFile);

            // Mostrar la imagen en el elemento img
            previewImage.src = imageUrl;
            previewImage.style.display = 'block';
        } else {
            // Limpiar la imagen si no es una imagen válida
            previewImage.src = '';
            previewImage.style.display = 'none';

            // Alerta al usuario sobre el tipo de archivo no admitido
            alert('Por favor, seleccione un archivo de imagen válido.');
        }
    }
}

function validarTipoImagen(fileType) {
    // Validar que el tipo de archivo sea una imagen
    return /^image\//.test(fileType);
};

let btnGuardar = document.getElementById("btnGuardar");
btnGuardar.addEventListener("click", (e) => {
    let dato = {};
    let validar = true;
    let Nombre = document.getElementById("Nombre");
    let Apellido = document.getElementById("Apellido");
    let Correo = document.getElementById("Correo");
    let password = document.getElementById("password");
    let inputFile = document.getElementById("inputFile").files;
    if (inputFile.length > 0) {
        dato.imagen = inputFile[0];
    }
    else {
        dato.imagen = null;
    }
    let expNombre = /^[a-zA-ZÀ-ÿ]+(?:\s[a-zA-ZÀ-ÿ]+)?(?:\s[a-zA-ZÀ-ÿ]+)?$/;
    let expApellido = /^[a-zA-ZÀ-ÿ]+(?:\s[a-zA-ZÀ-ÿ]+)?(?:\s[a-zA-ZÀ-ÿ]+)?$/;
    let exCorreo = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (expNombre.test(Nombre.value)) {
        document.getElementById("spanNombre").style.display = "none";
        dato.Nombre = Nombre.value;
        
    } else {
        document.getElementById("spanNombre").style.display = "block";
        validar = false;

    }
    if (expApellido.test(Apellido.value))
    {
        document.getElementById("spanApellido").style.display = "none";
        dato.Apellido = Apellido.value;
        
    } else {
        document.getElementById("spanApellido").style.display = "block";
        validar = false;
    }
    if (exCorreo.test(Correo.value)) {
        document.getElementById("spanCorreo").style.display = "none";
        dato.Correo = Correo.value;

    } else {
        document.getElementById("spanCorreo").style.display = "block";

        validar = false;
    }
    if (validar) {
        dato.id = document.getElementById("Id").value;
        dato.Password = password.value;
        let Perfil = new FormData();
        Perfil.append("Imagen", dato.imagen);
        Perfil.append("Nombre", dato.Nombre);
        Perfil.append("Apellido", dato.Apellido);
        Perfil.append("Correo", dato.Correo);
        Perfil.append("Password", dato.Password);
        Perfil.append("Id", dato.id);
        let xhr = new XMLHttpRequest();
        xhr.open("POST", "/Perfil/GuardarPerfil");
        xhr.addEventListener("load", e => {
            if (xhr.status >= 200 && xhr.status<300) {
                window.location.href = "/Home/Index";
                console.log(xhr.status)
            }
        })
        //xhr.setRequestHeader("Content-Type", "multipart/form-data")
        xhr.send(Perfil);
        console.log(dato);
        
    }
    
});