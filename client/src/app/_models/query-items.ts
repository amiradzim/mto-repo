import { ThemePalette } from '@angular/material/core';

export interface QueryItems {
    name: string;
    completed: boolean;
    color: ThemePalette;
    subtasks?: QueryItems[];
}
  