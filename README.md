# CodedVector.DddCommon
Common models for domain driven design

## Basic terms
- **Entity**: An object that is not defined by its attributes, but rather by a thread of continuity and its identity.  An entity is always in a valid state.
- **Value Object**: An object that contains attributes but has no conceptual identity. They should be treated as immutable. I suggest you use records for this.
- **Dto**: Data transfer object. It is an object that carries data between processes. It should be simple and contain no business logic.  This library assumes that there is a 'full' dto for every entity which can be used for both transfer and storage.
 