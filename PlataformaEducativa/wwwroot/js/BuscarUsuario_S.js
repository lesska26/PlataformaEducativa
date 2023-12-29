let centro = 0;
document.addEventListener("DOMContentLoaded", () => {
    let xhr = new XMLHttpRequest();
    xhr.open("GET", "/Usuario/DatosTbUsuario");
    xhr.addEventListener("load", () => {
        console.log(xhr.response);
        let json = JSON.parse(xhr.response);

        pTableDatos(json);
        document.getElementById("divLoading").classList.toggle("removeloading");
        let pagina = Math.ceil(json.cantidad / 10);
        for (let i = 1; i <= pagina; i++) {
            let aPaginacion = document.createElement("a");
            aPaginacion.textContent = i;
            aPaginacion.href = `#`;
            document.getElementById("paginacion").append(aPaginacion);
            aPaginacion.id = i;
            aPaginacion.addEventListener("click", paginacion)

        }
        console.log(json.cantidad);
    })
    xhr.send();


});
let btnBusqueda = document.getElementById("btnBusqueda");
btnBusqueda.addEventListener("click", filtar);

function filtar() {
    let selecteCentros = document.getElementById("selecteCentros");
    let selecteBusqueda = document.getElementById("selecteBusqueda");
    let inputBuq = document.getElementById("inputBuq");
    if (selecteCentros.value != 0) {
        let xhr = new XMLHttpRequest();
        let cedula = null;
        let nombre = null;
        console.log(selecteBusqueda.value)
        console.log(inputBuq.value);
        switch (selecteBusqueda.value) {
            case 0:

                break;
            case "2":
                nombre = inputBuq.value;
                console.log(inputBuq.value);
                break;
            case "1":
                cedula = inputBuq.value;
                console.log(inputBuq.value);
                break;
        }
        console.log(nombre);
        xhr.open("GET", `/Usuario/BuscarUsuario_Instituto?centro=${selecteCentros.value}&cedula=${cedula}&nombre=${nombre}`);
        xhr.addEventListener("load", () => {
            console.log(xhr.response);
            let json = JSON.parse(xhr.response);
            pTableDatos(json)
            let pagina = Math.ceil(json.cantidad / 10);
            document.getElementById("paginacion").innerHTML = null;
            centro = json.usuarios[0].institucionesId;
            for (let i = 1; i <= pagina; i++) {
                let aPaginacion = document.createElement("a");
                aPaginacion.textContent = i;
                aPaginacion.href = `#`;
                document.getElementById("paginacion").append(aPaginacion);
                aPaginacion.id = i;
                aPaginacion.addEventListener("click", paginacion)

            }

        })
        xhr.send();
    }

}

function paginacionBus(b) {
    let xhr = new XMLHttpRequest();
    let tbBody = document.getElementById("tb-body");
    tbBody.innerHTML = null;
    xhr.open("GET", `/Usuario/BuscarUsuario_Instituto?pg=${b.target.id}&centro=${centro}`);


    xhr.addEventListener("load", () => {
        let json = JSON.parse(xhr.response);
        document.getElementById("divLoading").classList.toggle("removeloading");

        console.log(json);
        pTableDatos(json);
    })
    document.getElementById("divLoading").classList.toggle("removeloading");
    xhr.send();


}

function paginacion(f) {
    let xhr = new XMLHttpRequest();
    let tbBody = document.getElementById("tb-body");
    tbBody.innerHTML = null;
    xhr.open("GET", `/Usuario/DatosTbUsuario?pg=${f.target.id}`);


    xhr.addEventListener("load", () => {
        let json = JSON.parse(xhr.response);
        document.getElementById("divLoading").classList.toggle("removeloading");

        console.log(json);
        pTableDatos(json);
    })
    document.getElementById("divLoading").classList.toggle("removeloading");
    xhr.send();
}

function pTableDatos(json) {
    let tbBody = document.getElementById("tb-body");
    tbBody.innerHTML = null;
    json.usuarios.forEach((e) => {
        let divBody = document.createElement("div");

        let nombreCompleto = `${e.nombre} ${e.apellido}`;
        let correo = e.correo;
        let telefono = e.telefono;
        let aOpciones = document.createElement("a");
        aOpciones.textContent = "Editar";
        aOpciones.href = `/Usuario/DatosTbUsuario?id=${e.usuarioId}`;
        let pNombre = document.createElement("p");
        let pCorreo = document.createElement("p");
        let pTelefono = document.createElement("p");
        pNombre.textContent = nombreCompleto;
        pCorreo.textContent = correo;
        pTelefono.textContent = telefono;
        divBody.append(pNombre, pCorreo, pTelefono, aOpciones);
        divBody.classList.add("divData");
        tbBody.append(divBody);
    })
}

