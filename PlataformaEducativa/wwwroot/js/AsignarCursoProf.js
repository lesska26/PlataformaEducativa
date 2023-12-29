document.querySelector("#select-P").addEventListener("click", e => {
    if (parseInt(e.target.value) != 0) {
        let pCorreo = document.querySelector("#correo");
        let pTelefono = document.querySelector("#telefono");
        let pSexo = document.querySelector("#sexo");
        let pNombre = document.querySelector("#nombre");
        let img = document.querySelector("#imag");
        var xhr = new XMLHttpRequest();
        xhr.open("POST", "/Usuario/buscarFacilitador", false)
        let UsuarioId = e.target.value;
        xhr.addEventListener("load", event => {
            console.log(xhr.response);
            if (xhr.status == 200) {
                let json = JSON.parse(xhr.response);
                pCorreo.innerHTML = `<strong>Correo:</strong> ${json.correo}`;
                pTelefono.innerHTML = `<strong>Telefono:</strong> ${json.telefono}`;
                pSexo.innerHTML = json.genero == 'M' ? "<strong>Sexo:</strong> Hombre" : "<strong>Sexo:</strong>Mujer";
                pNombre.textContent = json.nombre;
                var imagen = json.foto == null ? "descarga.png" : json.foto;
                img.setAttribute("src", `../img/${imagen}`);
                document.querySelector("#buttonAs").disabled = false;
            }
        })
        xhr.setRequestHeader("Content-Type", "Application/json");
        xhr.send(UsuarioId);

    }
})