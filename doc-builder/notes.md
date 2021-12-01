# Notes

## Entity Enum Property in TypeScript Models

```ts
enum DocItemType {
    Item = "Item",
    Question = "Question",
    Select = "Select"
}

var type = DocItemType.Question;

var question = "Question";

if (type.valueOf() === question) console.log('equal');
```