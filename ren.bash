#!/bin/bash

OLD="EntityMigrationFramework"
NEW="EntityMigrationFrameworkFramework"

# 1. Replace in file contents (excluding .git directory)
find . -type f ! -path "./.git/*" -exec sed -i "s/${OLD}/${NEW}/g" {} +

# 2. Rename files (depth-first, ignore .git)
find . -depth -type f -name "*${OLD}*" ! -path "./.git/*" | while IFS= read -r file; do
    newfile="$(dirname "$file")/$(basename "$file" | sed "s/${OLD}/${NEW}/g")"
    mv "$file" "$newfile"
done

# 3. Rename directories (depth-first, ignore .git)
find . -depth -type d -name "*${OLD}*" ! -path "./.git/*" | while IFS= read -r dir; do
    newdir="$(dirname "$dir")/$(basename "$dir" | sed "s/${OLD}/${NEW}/g")"
    mv "$dir" "$newdir"
done

