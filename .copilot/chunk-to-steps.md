# Converting a Chunk to a set of Steps

Break the chunk of functionality requested down into small steps. Each step should contain instructions I can pass to Copilot to implement some small piece of that chunk of functionality. Steps should build on each other. By the end of the last step, the entire chunk of functionality should be completed.

If a step involves writing C# or TypeScript code, that step should include instructions to use test-driven development to implement the step: start with writing a failing unit test(s) for the code, running the unit test(s), and confirming it fails with the expected error message. Then the step should include instructions for writing the code to satisfy the step and re-running the unit test(s) to ensure they pass.

If you can't use test-driven development to implement a step, include manual testing instructions for validating that the step has been completed.

Output the steps in Markdown, clearly separated from each other.
