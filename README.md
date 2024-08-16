**Microservice Event-Driven Architecture**

Welcome to the Microservice Event-Driven repository! This project demonstrates a microservices architecture utilizing event-driven communication patterns with a focus on RabbitMQ as the message broker.

**Table of Contents**

  Overview
  Features
  Architecture
  Technologies
  Getting Started
  Usage
  Contributing
  License
  Contact

**Prerequisites**
  **.NET 8 SDK
  Docker
  RabbitMQ
**
**Overview**

  The Microservice Event-Driven project provides a reference implementation of a microservices architecture where services communicate through events. 
  The system showcases how to integrate services using RabbitMQ and manage event-driven interactions to build scalable and resilient applications.

**Features**

  Microservices Architecture: Modular services that can be independently developed, deployed, and scaled.
  Event-Driven Communication: Uses RabbitMQ for event-based messaging between services, promoting loose coupling and scalability.
  Service Integration: Examples of service integration and event handling.
  Resilience and Fault Tolerance: Techniques for ensuring reliable communication and handling failures.

**Architecture
The architecture consists of:**
  
  Microservices: Individual services implementing different business functionalities.
  RabbitMQ: The message broker used for facilitating event-based communication between services.
  Event Producers: Services that publish events to RabbitMQ.
  Event Consumers: Services that subscribe to and process events from RabbitMQ.

**Technologies**

.NET 8: Framework used for building the microservices.
RabbitMQ: Message broker for handling event streams.
Docker: Containerization for easy deployment and scaling.
Docker Compose: Tool for defining and running multi-container Docker applications.

**Clone the Repository: **

**git clone https://github.com/ankitmori14/MicroserviceEventDriven.git
cd MicroserviceEventDriven
**

