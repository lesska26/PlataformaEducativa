document.addEventListener("DOMContentLoaded", () => {

    let selectMunicipio = document.getElementById("selectMunicipio");
    selectMunicipio.addEventListener("click", e => {
        if (e.target.value != 0) {
            let xhr = new XMLHttpRequest();
            xhr.open("Post", "/Asig_Prof/M_I");
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.addEventListener("load", f => {
                console.log(xhr.response)
                if (xhr.response != "false") {
                    let json = JSON.parse(xhr.response)
                    let select = document.getElementById("Centros");
                    select.disabled = false;
                    Array.from(select.options).forEach(option => {
                        select.remove(option.index);
                    })
                    let info = document.createElement("option");
                    info.value = 0;
                    info.text = "seleccionar un Centro";
                    document.getElementById("Centros").append(info);
                    json.forEach(r => {
                        let option = document.createElement("option")
                        option.value = r.institucionesId;
                        option.textContent = r.nombre;
                        document.getElementById("Centros").append(option);
                    })
                } else {
                    let select = document.getElementById("Centros");
                    Array.from(select.options).forEach(option => {
                        select.remove(option.index);
                    })
                    let info = document.createElement("option");
                    info.value = 0;
                    info.text = "seleccionar un Centro";
                    select.disabled = true;
                }
            });
            let Municipio = JSON.stringify(e.target.value);
            xhr.send(Municipio);

        } else {

            let select = document.getElementById("Centros");
            Array.from(select.options).forEach(option => {
                select.remove(option.index);
            })
            let info = document.createElement("option");
            info.value = 0;
            info.text = "seleccionar un Centro";
            select.append(info);
            select.disabled = true;
        }
        console.log(e.target.value)
    })

    let table = document.getElementById("T_P");

  let Centro = document.getElementById("Centros").addEventListener("click", r => {
        if (r.target.value != 0) {
           
            table.classList.add("table", "table-striped")
            //let Centros = document.getElementById("Centros");
            let xhr = new XMLHttpRequest();
            xhr.open("GET", `/Asig_Prof/B_P?Institucion=${r.target.value}`)
            xhr.addEventListener("load", () => {
                if (xhr.status >= 200 && xhr.status < 300) {
                    if (xhr.response != "false")
                    {
                        table.innerHTML = null;
                        let trEncabezado = document.createElement("tr");
                        let thFoto = document.createElement("th");
                        let thNombre = document.createElement("th");
                        let thMateria = document.createElement("th");
                        let thOpciones = document.createElement("th");
                        thFoto.textContent = "Foto";
                        thNombre.textContent = "Nombre";
                        thMateria.textContent = "Materia";
                        thOpciones.textContent = "Opciones";

                        trEncabezado.append(thFoto, thNombre, thMateria, thOpciones);
                        table.append(trEncabezado);
                        let json = JSON.parse(xhr.response);
                        console.log(json);
                        if (json == true) {
                            let P_div = document.getElementById("P_div");
                            P_div.innerHTML = null;
                            table.innerHTML = null;
                        } else {
                            json.forEach(e => {
                                let tdfoto = document.createElement("td");
                                let trDatos = document.createElement("tr");
                                let img = document.createElement("img");

                                var link = e.foto == null ? `descarga.png` : e.foto;
                                img.src = `/img/${link}`;
                                img.width = 100;
                                img.height = 100;
                                img.style.margin = "8px";
                                console.log(img);
                                console.log(e.foto);
                                tdfoto.append(img);
                                let tdNombre = document.createElement("td");
                                let Aopciones = document.createElement("a");
                                Aopciones.setAttribute("id", e.id);
                                Aopciones.setAttribute("href", `/Asig_Prof/A_S_M?id=${e.id}`);
                                Aopciones.setAttribute("class", "");
                                Aopciones.textContent = "Asignar Materia";
                                //Aopciones.setAttribute("data-bs-toggle", "modal");
                                //Aopciones.setAttribute("data-bs-target", "#exampleModal")
                                //Aopciones.addEventListener("click", EnviarDatos)
                                tdNombre.textContent = e.nombre;
                                tdfoto.textContent = e.foto;
                                trDatos.append(img, tdNombre);
                                let tdMateria = document.createElement("td");
                                e.materia.forEach(f => {
                                    tdMateria.innerHTML += ` ${f.cursosName}<b>.</b> `;
                                });
                                trDatos.append(tdMateria, Aopciones);
                                table.append(trDatos);
                            });
                            if (json.length > 0) {


                                let P_div = document.getElementById("P_div");
                                P_div.innerHTML = null;
                                let Principio_a = document.createElement("a");
                                let Final_a = document.createElement("a");
                                Principio_a.textContent = "Primero";
                                Final_a.textContent = "Final";
                                P_div.append(Principio_a);
                                let cout = json[0].cantidad / 10;
                                let paginacionNum = Math.ceil(cout);
                                console.log(paginacionNum)
                                for (let num = 1; num <= paginacionNum; num++) {
                                    let Paginacion = document.createElement("button");
                                    Paginacion.setAttribute("class", "btn btn-info");
                                    Paginacion.textContent = num;
                                    Paginacion.id = num;

                                    Paginacion.addEventListener("click", Paginacions)
                                    //Paginacion.href = `/Asig_Prof/B_P?Institucion=${Centros.value}&pg=${num}`
                                    P_div.append(Paginacion);
                                    console.log(Paginacion);

                                }

                                P_div.append(Final_a);
                                console.log(paginacionNum)
                            } 
                        }
                        
                       
                        console.log(json[0].cantidad);
                        
                        console.log(xhr.response)
                    }
                }
            })

            xhr.send();

        }
    })

    function Paginacions(e) {
        alert(e.target.id);
        let xhr = new XMLHttpRequest();
        let Centros = document.getElementById("Centros");
        xhr.open("GET", `/Asig_Prof/B_P?Institucion=${Centros.value}&pg=${e.target.id}`)
        xhr.addEventListener("load", () => {
            table.innerHTML = null;
            let trEncabezado = document.createElement("tr");
            let thFoto = document.createElement("th");
            let thNombre = document.createElement("th");
            let thMateria = document.createElement("th");
            let thOpciones = document.createElement("th");
            thFoto.textContent = "Foto";
            thNombre.textContent = "Nombre";
            thMateria.textContent = "Materia";
            thOpciones.textContent = "Opciones";

            trEncabezado.append(thFoto, thNombre, thMateria, thOpciones);
            table.append(trEncabezado);
            let json = JSON.parse(xhr.response);
            json.forEach(e => {
                let tdfoto = document.createElement("td");
                let trDatos = document.createElement("tr");
                let img = document.createElement("img");

                var link = e.foto == null ? `descarga.png` : e.foto;
                img.src = `/img/${link}`;
                img.width = 100;
                img.height = 100;
                img.style.margin = "8px";
                console.log(img);
                console.log(e.foto);
                tdfoto.append(img);
                let tdNombre = document.createElement("td");
                let Aopciones = document.createElement("a");
                Aopciones.setAttribute("id", e.id);
                Aopciones.setAttribute("href", `/Asig_Prof/A_S_M?id=${e.id}`);
                Aopciones.setAttribute("class", "");
                Aopciones.textContent = "Asignar Materia";
                //Aopciones.setAttribute("data-bs-toggle", "modal");
                //Aopciones.setAttribute("data-bs-target", "#exampleModal")
                //Aopciones.addEventListener("click", EnviarDatos)
                tdNombre.textContent = e.nombre;
                tdfoto.textContent = e.foto;
                trDatos.append(img, tdNombre);
                let tdMateria = document.createElement("td");
                e.materia.forEach(f => {
                    tdMateria.innerHTML += ` ${f.cursosName}<b>.</b> `;
                });
                trDatos.append(tdMateria, Aopciones);
                table.append(trDatos);
                console.log(xhr.response);

            });
        })
        xhr.send();

    }   

    function EnviarDatos(id,datos=null)
    {
        console.log(id.target.id);
        let inputId = document.getElementById("profesorId");
        inputId.value = id.target.id;
        let xhr = new XMLHttpRequest();
        xhr.open("GET", `/Asig_Prof/A_S_M?id=${id.target.id}`)
        xhr.addEventListener("load", () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                let materia = document.getElementById("Materia");
                let otros = document.getElementById("otros");
                let pMateria = document.createElement("p");
                pMateria.textContent = "Materia";
                let pOtros = document.createElement("p");
                pOtros.textContent = "Agregar Materia";
                otros.innerHTML = null;
                materia.innerHTML = null;
                otros.append(pOtros);
                materia.append(pMateria);
                if (xhr.response=="false") {
                    
                    console.log(xhr.response)
                    return false;
                }
                console.log(xhr.response);
                let json = JSON.parse(xhr.response);
                json.materia.forEach(f => {
                    let label = document.createElement("label");
                    label.textContent = f.materiaName;
                    let inputcheck = document.createElement("input");
                    inputcheck.setAttribute("type", "checkbox");
                    inputcheck.value = f.materiaId;
                    inputcheck.checked = true;
                    materia.append(label, inputcheck);
                })

                json.materiaFaltante.forEach(f => {
                    let label = document.createElement("label");
                    label.textContent = f.materiaName;
                    let inputcheck = document.createElement("input");
                    inputcheck.setAttribute("type", "checkbox");
                    inputcheck.value = f.materiaId;
                  
                    otros.append(label, inputcheck);
                })
            }
        })
        xhr.send();

    }
    //let guardar = document.getElementById("guardar");
    //guardar.addEventListener("click", () => {
    //    let listMateriaDelete = document.querySelector("#Materia").querySelectorAll("input");
    //    let delet=[];
    //    listMateriaDelete.forEach(e => {
    //        if (!e.checked)
    //            delet.push(e.value);
    //    })
    //    let listMateriaAgregar = document.querySelector("#otros").querySelectorAll("input");
    //    let agregar = [];
    //    listMateriaAgregar.forEach(e => {
    //        if (e.checked)
    //            agregar.push(e.value);
                
    //    })
    //    var datos = {
    //        delet,
    //        agregar
    //    };
    //    console.log(datos);
    //    let json = JSON.stringify(datos);
    //    let xhr = new XMLHttpRequest();

    //    xhr.open("POST", "/Asig_Prof/DeleteUpgradeMateria");
    //    xhr.setRequestHeader("Content-Type", "application/json");
    //    xhr.addEventListener("load", f => {
    //        if (xhr.status >= 200 && xhr.status < 300) {
    //            console.log(xhr.response)
    //        }
    //    });
    //    xhr.send(json);
    //});

});


