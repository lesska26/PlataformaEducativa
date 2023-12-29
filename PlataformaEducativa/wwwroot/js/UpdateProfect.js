let guardar = document.getElementById("Actualizar");
guardar.addEventListener("click", () => {
        let listMateriaDelete = document.querySelector("#Materia").querySelectorAll("input");
        let delet=[];
        listMateriaDelete.forEach(e => {
            if (!e.checked)
                delet.push(e.value);
        })
        let listMateriaAgregar = document.querySelector("#otros").querySelectorAll("input");
        let agregar = [];
        let profesorId = document.getElementById("profesorId").value
        listMateriaAgregar.forEach(e => {
            if (e.checked)
                agregar.push(e.value);

        })
        var datos = {
            delet,
            agregar,
            profesorId

        };
        console.log(datos);
        let json = JSON.stringify(datos);
        let xhr = new XMLHttpRequest();

        xhr.open("POST", "/Asig_Prof/DeleteUpgradeMateria");
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.addEventListener("load", f => {
            if (xhr.status >= 200 && xhr.status < 300) {
                window.location.href = "/Asig_Prof/M_I";
            }
        });
        xhr.send(json);
    });