import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main/main.component';
import { QueryFormComponent } from './query-form/query-form.component';
import { UserGuideComponent } from './user-guide/user-guide.component';
import { PublicTableComponent } from './public-table/public-table.component';
import { AuthGuard } from './_guards/auth.guard';
import { UploadFileComponent } from './upload-file/upload-file.component';

const routes: Routes = [
  {
    path: '', component: MainComponent
  },

  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'public-table', component: PublicTableComponent
      },
    
      {
        path: 'query-form', component: QueryFormComponent
      },

      {
        path: 'upload-file', component: UploadFileComponent
      }
    ]
  },

  {
    path: 'user-guide', component: UserGuideComponent
  },

  // catch all or wild card root to reroute back to main component 
  {
    path: '**', component: MainComponent , pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
