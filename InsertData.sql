-- Script SQL de Insercion de Datos para AutoTallerManager (Datos Multiples)

-- Crear base de datos si no existe
CREATE DATABASE IF NOT EXISTS SGTA;

-- Ingresar a la base de datos
USE SGTA;

-- Inserciones

-- Tabla: TypeService
INSERT INTO TypeService (Name, Description)
VALUES ('Mantenimiento Preventivo', 'Servicios periodicos para mantener el vehiculo en optimas condiciones'),
       ('Reparacion', 'Reparacion de componentes averiados del vehiculo'),
       ('Diagnostico', 'Evaluacion del estado general del vehiculo');

-- Tabla: State
INSERT INTO State (Name)
VALUES ('Pendiente'),
       ('En Proceso'),
       ('Completada'),
       ('Cancelada');

-- Tabla: Role
INSERT INTO Role (Name)
VALUES ('Admin'),
       ('Mecanico'),
       ('Recepcionista');

-- Tabla: User
INSERT INTO User (Name, LastName, UserName, Email, Password)
VALUES ('Elena', 'Castro', 'ecastrot', 'elena@gmail.com', 'hashedPassword4'),
       ('Jorge', 'Mejia', 'jmejia', 'jorge@gmail.com', 'hashedPassword5'),
       ('Laura', 'Velasco', 'lvelasco', 'laura@gmail.com', 'hashedPassword6'),
       ('Isa', 'Ramirez', 'isaram', 'isa@gmail.com', 'hashedPassword10'),
       ('Oscar', 'Gomez', 'ogomez', 'oscar@gmail.com', 'hashedPassword11');

-- Tabla: UserRole (Asociacion Usuario - Rol)
INSERT INTO UserRole (UserId, RoleId)
VALUES (1, 1),
       (2, 2),
       (3, 2),
       (4, 3),
       (5, 3);

-- Tabla: Client
INSERT INTO Client (Name, LastName, Phone, Email, BirthDate, Identification)
VALUES ('Julian', 'Salazar', '3211234567', 'julian@gmail.com', '1992-04-30', '4567890123'),
       ('Daniela', 'Moreno', '3005678910', 'daniela@gmail.com', '1988-11-20', '5678901234'),
       ('Mateo', 'Ruiz', '3117890123', 'mateo@gmail.com', '1995-07-18', '6789012345'),
       ('Isa', 'Perez', '3224567890', 'isaperez@gmail.com', '1990-03-12', '9876543210'),
       ('Oscar', 'Vargas', '3123456789', 'oscarvargas@gmail.com', '1987-05-09', '6543219870');

-- Tabla: Vehicle
INSERT INTO Vehicle (ClientId, Brand, Model, VIN, Mileage)
VALUES (4, 'Renault', 'Logan', '111AAA222BBB333CCC', 45000),
       (5, 'Hyundai', 'i10', '444DDD555EEE666FFF', 38000),
       (6, 'Kia', 'Sportage', '777GGG888HHH999III', 15000),
       (7, 'Chevrolet', 'Sail', 'ISA1234CARVIN9999', 23000),
       (8, 'Mazda', '3', 'VINMAZDA333999AAA', 41000);

-- Tabla: SpacePart (Repuestos)
INSERT INTO SpacePart (Code, Description, Stock, UnitPrice)
VALUES ('REP-001', 'Filtro de aceite', 50, 35000),
       ('REP-002', 'Pastillas de freno', 30, 80000),
       ('REP-003', 'Filtro de aire', 25, 40000),
       ('REP-004', 'Aceite sintetico 5W30', 40, 45000),
       ('REP-005', 'Bujias NGK', 60, 50000);

-- Tabla: ServiceOrder
INSERT INTO ServiceOrder (VehicleId, TypeServiceId, ClientId, StateId, EntryDate, IsAuthorized, ClientMessage)
VALUES (4, 1, 4, 1, NOW(), TRUE, 'Revision de suspension'),
       (5, 2, 5, 2, NOW(), TRUE, 'Problemas con la direccion'),
       (6, 3, 6, 3, NOW(), TRUE, 'Revision preventiva general'),
       (7, 1, 7, 1, NOW(), TRUE, 'Cambio de aceite'),
       (8, 2, 8, 2, NOW(), TRUE, 'Ruido extrano en el motor');

-- Tabla: Invoice
INSERT INTO Invoice (ServiceOrderId, TotalPrice, Date)
VALUES (4, 180000, NOW()), (5, 95000, NOW()), (6, 200000, NOW()), (7, 85000, NOW()), (8, 145000, NOW());

-- Tabla: OrderDetails
INSERT INTO OrderDetails (ServiceOrderId, SpacePartId, RequiredPieces, TotalPrice)
VALUES (4, 1, 1, 35000), (5, 2, 1, 80000), (6, 3, 1, 120000), (7, 4, 1, 45000), (8, 5, 2, 100000);

-- Tabla: Diagnostic
INSERT INTO Diagnostic (UserId, Description)
VALUES (1, 'Rotula desgastada'), (2, 'Direccion desalineada'), (3, 'Filtro de aire saturado'), (4, 'Aceite contaminado'), (5, 'Bujia defectuosa');

-- Tabla: DetailsDiagnostic
INSERT INTO DetailsDiagnostic (ServiceOrderId, DiagnosticId)
VALUES (4, 4), (5, 5), (6, 6), (7, 7), (8, 8);

-- Tabla: Auditory
INSERT INTO Auditory (UserId, Entity, Date, TypeAction)
VALUES (1, 'Client', NOW(), 'INSERT'), (2, 'Vehicle', NOW(), 'UPDATE'), (3, 'ServiceOrder', NOW(), 'DELETE'), (4, 'Invoice', NOW(), 'INSERT'), (5, 'OrderDetails', NOW(), 'UPDATE');
