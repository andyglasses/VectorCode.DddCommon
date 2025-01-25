# VectorCode.DddCommon
Common models for domain driven design

## Basic terms
- **Entity**: An object that is not defined by its attributes, but rather by a thread of continuity and its identity.  An entity is always in a valid state.
- **Value Object**: An object that contains attributes but has no conceptual identity. They should be treated as immutable. I suggest you use records for this.
- **Dto**: Data transfer object. It is an object that carries data between processes. It should be simple and contain no business logic.  This library assumes that there is a 'full' dto for every entity which can be used for both transfer and storage.
- **Repository**: An object that abstracts the storage of entities.  It should be used to retrieve and store entities.
- **Aggregate Root**: An entity that is the root of an aggregate.  It is the only entity that can be accessed from outside the aggregate.  You can ignore aggregate root if the logic help within BaseEntity is suitable for your needs.
- **Domain Event**: An event that is raised by an entity.  It is used to communicate changes to other parts of the system.  These events should be handle din your application layer


## Thoughts on DDD
- Domain Entities (or aggregates) should be designed to be in a valid state at all times.  This means that the entity should be responsible for ensuring that it is in a valid state.  