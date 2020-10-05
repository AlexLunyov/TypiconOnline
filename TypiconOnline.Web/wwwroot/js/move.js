//var move = function (array, element, delta) {
//    var index = array.indexOf(element);
//    var newIndex = index + delta;
//    if (newIndex < 0 || newIndex == array.length) return; //Already at the top or bottom.
//    var indexes = [index, newIndex].sort(); //Sort the indixes
//    array.splice(indexes[0], 2, array[indexes[1]], array[indexes[0]]); //Replace from lowest index, two elements, reverting the order
//};

var move = function (array, index, delta) {
    moveInner(array, index, index + delta);
};

function moveInner(arr, old_index, new_index) {
    while (old_index < 0) {
        old_index += arr.length;
    }
    while (new_index < 0) {
        new_index += arr.length;
    }
    if (new_index >= arr.length) {
        var k = new_index - arr.length;
        while ((k--) + 1) {
            arr.push(undefined);
        }
    }
    arr.splice(new_index, 0, arr.splice(old_index, 1)[0]);
    return arr;
}