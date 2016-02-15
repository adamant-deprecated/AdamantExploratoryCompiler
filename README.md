# Adamant Exploratory Compiler
This is an exploratory compiler for the Adamant programming language.  It exists to rapidly evaluate language design decisions and possibly form the basis for bootstrapping an Adamant compiler.  The exploratory compiler will be thrown away when the compiler is re-written in Adamant.

## Project Status: Alpha Active
This is under active development.  It is in a very early stage and there are likely issues and limitations.  APIs are subject to frequent breaking changes.

### Download and Use
Clone this git repo and compile using Visual Studio 2015.

## Explanation of this Project
The Adamant language is being designed.  It is being created to provide a high-level OO language (like C# or Java) using a borrow checker instead of garbage collection (like Rust).  Planned features include:

  * write once, compile anywhere
  * guaranteed optimizations
  * asynchronous IO
  * type inference
  * generics with partial specialization
  * object lifetimes
  * class defined interfaces
  * optional exception specifications
  * minimal runtime

Since there is no comparable language available at this time it is important to rapidly test the design of the language and possibly iterate on that design.  This project is intended to be the platform for that.

Goals:
  1. Rapid Development
  2. Flexible to Syntax, Grammar and Semantics changes
  3. Enforce All Language Syntax, Grammar and Semantics
  4. Executable Output

No attempt is being made to make this project easy to port to Adamant.  In fact, it is expected it won't be because rapid development is a much higher priority that is at odds with this.

Note, this is the third attempt at creating a compiler for Adamant.  The first was the "[BootstrapCompiler](https://github.com/adamant-deprecated/AdamantBootstrapCompiler)", whose approach of direct translation to C# without symbol and semantic validation simply didn't work.  The second was the "[AdamantTemporaryCompiler](https://github.com/adamant-deprecated/AdamantTemporaryCompiler)" which was not providing rapid enough development and language prototyping. That project bogged down attempting to write a lexer generator.