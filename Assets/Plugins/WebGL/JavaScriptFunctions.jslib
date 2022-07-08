mergeInto(LibraryManager.library, {

    Save: function() {
        FS.syncfs(false, function (err) { });
        console.log("Save synced");
    }

});