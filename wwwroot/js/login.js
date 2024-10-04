document.getElementById('loginForm').addEventListener('submit', async function (e) {
    e.preventDefault(); // Evitar el envío del formulario

    const requestData = {
        CorreoElectronico: document.getElementById('correoElectronico').value,
        Password: document.getElementById('password').value
    };

    const response = await fetch('api/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(requestData)
    });

    if (response.ok) {
        const result = await response.json();
        if (result.Role === "Empleado") {
            // Redirigir a la vista de empleado
            window.location.href = "empleado.html"; // Ruta correcta para la vista de empleado
        } else if (result.Role === "Cliente") {
            // Redirigir a la vista de cliente
            window.location.href = "cliente.html"; // Ruta correcta para la vista de cliente
        }
    } else {
        // Manejar error de inicio de sesión
        alert("Credenciales inválidas");
    }
});
