-- Script SQL de Insercion de Datos para AutoTallerManager (Datos Multiples)

-- Ingresar a la base de datos
\c sgta

-- Inserciones

-- Tabla: TypeService
INSERT INTO type_service (name, duration, price)
VALUES ('Mantenimiento Preventivo', 8, 45000),
       ('Reparacion', 12, 50000),
       ('Diagnostico', 5, 23000);

-- Tabla: State
INSERT INTO state (name)
VALUES ('Pendiente'),
       ('En Proceso'),
       ('Completada'),
       ('Cancelada');

-- Tabla: Role
INSERT INTO roles ("Description")
VALUES ('Administrator'),
       ('Mechanic'),
       ('Receptionist');

-- Tabla: User
INSERT INTO users (name, lastname, username, email, password)
VALUES ('Elena', 'Castro', 'ecastrot', 'elena@gmail.com', 'hashedPassword4'),
       ('Jorge', 'Mejia', 'jmejia', 'jorge@gmail.com', 'hashedPassword5'),
       ('Laura', 'Velasco', 'lvelasco', 'laura@gmail.com', 'hashedPassword6'),
       ('Isa', 'Ramirez', 'isaram', 'isa@gmail.com', 'hashedPassword10'),
       ('Oscar', 'Gomez', 'ogomez', 'oscar@gmail.com', 'hashedPassword11');

-- Tabla: UserRole (Asociacion Usuario - Rol)
INSERT INTO "RolUser" ("UsersId", "RolsId")
VALUES (1, 1),
       (2, 2),
       (3, 2),
       (4, 3),
       (5, 3);

-- Tabla: Client
INSERT INTO clients (name, lastname, phone, email, birth, identification)
VALUES ('Julian', 'Salazar', '3211234567', 'julian@gmail.com', '1992-04-30', '4567890123'),
       ('Daniela', 'Moreno', '3005678910', 'daniela@gmail.com', '1988-11-20', '5678901234'),
       ('Mateo', 'Ruiz', '3117890123', 'mateo@gmail.com', '1995-07-18', '6789012345'),
       ('Isa', 'Perez', '3224567890', 'isaperez@gmail.com', '1990-03-12', '9876543210'),
       ('Oscar', 'Vargas', '3123456789', 'oscarvargas@gmail.com', '1987-05-09', '6543219870');

INSERT INTO type_vehicle(name) VALUES ('Carro'), ('Camioneta');
-- Tabla: Vehicle
INSERT INTO vehicles (client_id, brand, model, vin, mileage, type_vehicle_id)
VALUES (1, 'Renault', 'Logan', '111AAA222BBB333CCC', 45000, 1),
       (2, 'Hyundai', 'i10', '444DDD555EEE666FFF', 38000, 1),
       (3, 'Kia', 'Sportage', '777GGG888HHH999III', 15000, 1),
       (4, 'Chevrolet', 'Sail', 'ISA1234CARVIN9999', 23000, 2),
       (5, 'Mazda', '3', 'VINMAZDA333999AAA', 41000, 2);

-- Tabla: SpacePart (Repuestos)
INSERT INTO spare_part (code, description, stock, unit_price, min_stock, max_stock, category)
VALUES ('REP-001', 'Filtro de aceite', 50, 35000, 10, 70, 'Motor'),
       ('REP-002', 'Pastillas de freno', 30, 80000, 10, 70, 'Frenos'),
       ('REP-003', 'Filtro de aire', 25, 40000, 10, 70, 'Motor'),
       ('REP-004', 'Aceite sintetico 5W30', 40, 45000, 10, 70, 'Aceites'),
       ('REP-005', 'Bujias NGK', 60, 50000, 10, 70, 'Motor');

-- Tabla: ServiceOrder

INSERT INTO service_order (
    vehicles_id, 
    type_service_id, 
    "ClientId", 
    state_id, 
    entry_date, 
    is_authorized, 
    client_message, 
    exit_date, 
    "UserId"
)
VALUES 
    (1, 1, 1, 1, '2025-06-15', TRUE, 'Revision de suspension', '2025-06-17', 1),
    (2, 2, 2, 2, '2025-06-16', TRUE, 'Problemas con la direccion', '2025-06-18', 2),
    (3, 3, 3, 3, '2025-06-17', TRUE, 'Revision preventiva general', '2025-06-20', 3),
    (4, 1, 4, 1, '2025-06-18', TRUE, 'Cambio de aceite', '2025-06-23', 4),
    (5, 2, 5, 2, '2025-06-19', TRUE, 'Ruido extrano en el motor', '2025-06-22', 5);

-- Tabla: Invoice
INSERT INTO invoice (service_order_id, total_price, date)
VALUES (1, 180000, NOW()), (2, 95000, NOW()), (3, 200000, NOW()), (4, 85000, NOW()), (5, 145000, NOW());

-- Tabla: OrderDetails
INSERT INTO order_details (service_order_id, spare_part_id, required_pieces, total_price)
VALUES (1, 1, 1, 35000), (2, 2, 1, 80000), (3, 3, 1, 120000), (4, 4, 1, 45000), (5, 5, 2, 100000);

-- Tabla: Diagnostic
INSERT INTO diagnostics (user_id, description, date)
VALUES (1, 'Rotula desgastada', '2025-05-10'), (2, 'Direccion desalineada', '2025-05-10'), (3, 'Filtro de aire saturado', '2025-05-10'), (4, 'Aceite contaminado', '2025-05-10'), (5, 'Bujia defectuosa', '2025-05-10');

-- Tabla: DetailsDiagnostic
INSERT INTO details_diagnostics ("ServiceOrderId", "DiagnosticId")
VALUES (1, 1), (2, 2), (3, 3), (4, 4), (5, 5);

INSERT INTO inspection (name) VALUES ('Frenos'), ('Motor'), ('Luces'), ('Retrovisor'), ('Volante');

INSERT INTO detaill_inspection ("serviceOrder_id", inspection_id, quantity) VALUES (1, 1, 2), (2, 2, 4);

INSERT INTO specializations (name) VALUES ('Automotriz'), ('Vehiculos pesados');

INSERT INTO user_specializations ("UserId", "SpecializationId") VALUES (2, 1), (3, 2);