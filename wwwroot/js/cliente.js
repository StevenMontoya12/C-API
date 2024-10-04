document.addEventListener('DOMContentLoaded', async function () {
    // Obtener el clienteID desde localStorage
    const clienteID = localStorage.getItem('clienteID');
    console.log("ClienteID desde localStorage:", clienteID);

    if (!clienteID) {
        console.log("No se encontró el clienteID en localStorage. Redirigiendo a index.html...");
        window.location.href = "index.html"; // Redirigir si no hay clienteID en localStorage
        return;
    }

    try {
        // Hacer una solicitud GET a la API para obtener los datos del cliente
        const response = await fetch(`http://localhost:5085/api/cliente/${clienteID}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        console.log("Estado de la respuesta de la API:", response.status); // Verificar el estado de la respuesta
        if (response.ok) {
            const cliente = await response.json(); // Obtener los datos del cliente en formato JSON
            console.log("Datos del cliente:", cliente);

            // Rellenar los campos con la información del cliente
            document.getElementById('nombreCliente').textContent = cliente.nombre;
            document.getElementById('clienteID').textContent = cliente.clienteID;
            document.getElementById('nombreCompleto').textContent = `${cliente.nombre} ${cliente.apellido}`;
            document.getElementById('correoCliente').textContent = cliente.correoElectronico;
        } else {
            console.error('Error al obtener los datos del cliente:', response.statusText);
            alert("Error al cargar los datos del cliente.");
        }
    } catch (error) {
        console.error('Error de red:', error);
        alert("Error al conectarse con el servidor.");
        window.location.href = "index.html"; // Redirigir en caso de error
    }
});
