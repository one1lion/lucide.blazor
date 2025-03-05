# Contributing to InfiniLore.Lucide

We welcome contributions to InfiniLore.Lucide! This document outlines the process and guidelines for contributing to this project.

## Getting Started

1.  **Fork the repository:** Click the "Fork" button at the top right of the page to create your own copy of the repository.
2.  **Clone your fork:**
    ```bash
    git clone [https://github.com/your-username/InfiniLore.Lucide.git](https://www.google.com/search?q=https://github.com/your-username/InfiniLore.Lucide.git)
    cd InfiniLore.Lucide
    ```
3.  **Create a branch:** Create a new branch for your changes.
    ```bash
    git checkout -b feature/your-feature-name
    ```

## Development Environment Setup

### Prerequisites

* .NET 9

### Installation

1.  Navigate to the solution directory.
2.  Install npm dependencies:
    ```bash
    npm install
    ```
3.  Restore the dotnet tools that are described in the tool manifest.
    ```bash
    dotnet tool restore
    ```

## Building the Project

1.  Navigate to the solution directory.
2.  Build the project using the .NET CLI:
    ```bash
    dotnet build
    ```
3.  Alternatively, you can use the Cake build script:
    ```bash
    dotnet dotnet-cake --target=Test --rebuild
    ```
    Refer to the `build.cake` script for more detailed build information.

## Running Tests

1.  Navigate to the solution directory.
2.  Run the tests using the .NET CLI:
    ```bash
    dotnet test tests/Tests.InfiniLore.Lucide
    ```

## Coding Standards

* Please follow the existing coding style and conventions used in the project.
* Use the .NET coding standards.

## Making Changes

1.  Make your changes to the codebase.
2.  **Commit your changes:**
    ```bash
    git add .
    git commit -m "Add your descriptive commit message"
    ```
3.  **Push your changes to your fork:**
    ```bash
    git push origin feature/your-feature-name
    ```

## Submitting a Pull Request

1.  Go to the original repository on GitHub.
2.  Click the "New Pull Request" button.
3.  Ensure that your branch is being compared to the `core` (or `develop`) branch of the original repository.
4.  Provide a clear and descriptive title and description for your pull request.
5.  If your pull request addresses an existing issue, include the issue number in the description (e.g., "Fixes #123").
6.  Submit the pull request.

## Issue Reporting

* If you find a bug or have a feature request, please open an issue on GitHub.
* Provide as much detail as possible, including:
    * Steps to reproduce the issue.
    * Expected behavior.
    * Actual behavior.
    * Relevant error messages.
    * Your environment (operating system, version, etc.).

## Code of Conduct

Please review and adhere to our [Code of Conduct](CODE_OF_CONDUCT.md).

## Questions?

If you have any questions, please feel free to ask them in the issues or discussions.

Thank you for contributing to InfiniLore.Lucide!