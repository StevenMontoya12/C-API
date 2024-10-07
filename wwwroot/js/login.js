document.getElementById('loginForm').addEventListener('submit', async function (e) {
    e.preventDefault(); // Evitar el envío del formulario

    // Verificar si el botón clicado es el de iniciar sesión
    const clickedButton = e.submitter || document.activeElement;

    if (clickedButton.name === 'login') { // Solo proceder si es el botón de "Iniciar Sesión"
        const correoElectronico = document.getElementById('correoElectronico').value;
        const password = document.getElementById('password').value;

        // Debug: Verificar si los campos están siendo capturados
        console.log("Correo electrónico:", correoElectronico);
        console.log("Contraseña:", password);

        if (!correoElectronico || !password) {
            alert("Por favor, complete ambos campos antes de iniciar sesión.");
            return;
        }

        const requestData = {
            CorreoElectronico: correoElectronico,
            Password: password
        };

        try {
            // Enviar la solicitud POST a la API de login
            const response = await fetch('/api/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(requestData)
            });

            if (response.ok) {
                const result = await response.json();
                console.log('Respuesta del servidor:', result);

                // Verificar el rol del usuario y redirigir a la vista correspondiente
                if (result.role === "Empleado") {
                    window.location.href = "/vista/empleado.html";
                } else if (result.role === "Cliente") {
                    window.location.href = "/vista/cliente.html";
                } else {
                    console.log("Rol desconocido:", result.role);
                }
            } else {
                alert("Credenciales inválidas");
            }
        } catch (error) {
            console.error("Error al realizar la solicitud:", error);
            alert("Error al conectarse con el servidor.");
        }
    } else {
        console.log("El botón de 'Registrarse' fue presionado, no se ejecuta el login.");
    }
});
