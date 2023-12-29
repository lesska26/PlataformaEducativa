let spanNombre = document.getElementById("spanNombre");
let spanTelefno = document.getElementById("spanTelefono");
let spanCorreo = document.getElementById("spanCorreo");
let pNombre = document.getElementById("pNombre");
let imagen = document.querySelector("#imagen");
document.getElementById("profesor").addEventListener("change", e => {
    if (e.target.value != 0) {
        let xhr = new XMLHttpRequest();
        xhr.open("GET", `/Asig_Prof/BuscarProfesor?id=${e.target.value}`,true);
        xhr.addEventListener("load", () => {
            console.log(xhr.response);
            let json = JSON.parse(xhr.response);
            spanNombre.textContent = json.nombre;
            pNombre.textContent = json.nombre;
            spanCorreo.textContent = json.correo;
            spanTelefno.textContent = json.telefono;
            let imagens = json.foto;
            imagen.setAttribute("src", `../img/descarga.png`);
        })
        xhr.send();
    }
})