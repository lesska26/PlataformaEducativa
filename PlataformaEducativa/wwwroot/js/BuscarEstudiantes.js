document.getElementById("b-Est").addEventListener("click", e => {
    let buscar = document.getElementById("ipt-busc");
    let xhr = new XMLHttpRequest();
    let loadin = document.querySelector("#loading");
    document.querySelector("#table-info").innerHTML = null;
    loadin.style.display = "block";
    xhr.open("POST", "/Estudiantes/BucardorEstudiantes");
    xhr.addEventListener("load", () => {
        console.log(xhr.response);

        if (xhr.response!="")
        {
            document.querySelector("#table-info").innerHTML = null;
            let trs = document.createElement("tr");
            let curso = document.createElement("td")
            let Hora = document.createElement("td")
            let Certificado = document.createElement("td");
            curso.textContent = "Curso";
            Hora.textContent = "Hora";
            Certificado.textContent = "Descargar";
            trs.append(curso);
            trs.append(Hora);
            trs.append(Certificado);
            console.log(trs);
            document.querySelector("#table-info").append(trs);
            let json = JSON.parse(xhr.response);
            document.getElementById("name-est").textContent = json.nombre;
            json.listaCurso.forEach(f => {
                let tr = document.createElement("tr");
                let tdcurso = document.createElement("td");
                let tdHora = document.createElement("td");
                let ACertificado = document.createElement("a");
                ACertificado.text = "Certificado";
                tdcurso.textContent = f.curso;
                tdHora.textContent = f.duracion;
                // ACertificado.setAttribute("asp-route-IniciarCursoId", f.iniciarCursoId);
                // ACertificado.setAttribute("asp-route-EstudiantesId", json.estudiantesId);
                // ACertificado.setAttribute("asp-controller", "Certificados");
                // ACertificado.setAttribute("asp-action", "Certificado")
                ACertificado.setAttribute("href", `/Certificados/Certificado?IniciarCursoId=${f.iniciarCursoId}&EstudiantesId=${json.estudiantesId}`)
                ACertificado.setAttribute("class", "btn btn-danger");

                tr.append(tdcurso, tdHora, ACertificado);
                document.querySelector("#table-info").append(tr);

                console.log(f.curso)
            })
        }
        
        loadin.style.display = "none";
        document.querySelector("#table-info").style.display = "inline-table";

    })
    let NCM = {
        NCM: buscar.value
    };

    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(JSON.stringify(NCM));
})