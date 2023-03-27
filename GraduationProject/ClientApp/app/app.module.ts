import { CitiesService } from './services/cities.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_BASE_HREF } from '@angular/common';
import {MatCardModule} from '@angular/material/card';
import {MatDividerModule} from '@angular/material/divider'
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatProgressBarModule} from '@angular/material/progress-bar';


import { AppComponent } from './app.component';
import { NavbarComponent } from './components/home/navbar/navbar.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { HomeComponent } from './components/home/home.component';
import { AreasService } from './services/areas.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatInputModule} from '@angular/material/input';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { LoginComponent } from './components/user/login/login.component';
import { UserService } from './services/user.service';
import { AuthGuard } from './guards/auth.guard';
import { AuthInterceptor } from './guards/auth-interceptor';
import { Routes, RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { ProfileComponent } from './components/profile/profile.component';
import { UserdetailsComponent } from './components/profile/userdetails/userdetails.component';
import { ReviewsComponent } from './components/profile/reviews/reviews.component';
import { UserproductComponent } from './components/profile/userproduct/userproduct.component';
import { ProductCardComponent } from './components/resuable/product-card/product-card.component';
import { UserInfoService } from './services/userInfo.service';
import { RatingComponent } from './components/resuable/rating/rating.component';
import {MatButtonModule} from '@angular/material/button';
import {MatButtonToggleModule} from '@angular/material/button-toggle'
import { MatDialogModule } from '@angular/material/dialog';
import {MatIconModule } from '@angular/material/icon';
import {ReactiveFormsModule } from '@angular/forms';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatStepperModule} from '@angular/material/stepper';
import { AddProductComponent } from './components/products/add-product/add-product.component'
import { ProductService } from './services/product.service';
import { ImageCropperModule } from 'ngx-image-cropper';
import { ListProductsComponent } from './components/products/list-products/list-products.component';
import { NgxEditorModule } from 'ngx-editor';
import { ProductDetailsComponent } from './components/products/product-details/product-details.component';
import { ChatComponent } from './components/chat/chat.component';
import { AvatarModule } from 'ngx-avatar';
import { ResetPasswordComponent } from './components/user/reset-password/reset-password.component';
import { EmailSentComponent } from './components/user/email-sent/email-sent.component';
import { UserSettingsComponent } from './components/profile/usersettings/usersettings.component';
import { BuyerService } from './services/buyer.service';
import { BuyersComponent } from './components/profile/buyers/buyers.component';
import { ProductOwnerGuard } from './guards/product-owner.guard';
import { RatingDailogComponent } from './components/resuable/rating-dailog/rating-dailog.component';
import { SuggestionsComponent } from './components/suggestions/suggestions.component';
import { SuggestionsService } from './services/suggestions.service';
import { UserReviewsComponent } from './components/user/user-reviews/user-reviews.component';
import { FooterComponent } from './components/home/footer/footer.component';
import { WishlistComponent } from './components/user/wishlist/wishlist.component';
import { WishlistService } from './services/wishlist.service';
import { ChartsModule } from 'ng2-charts';
import { CategoryFilterComponent } from './components/products/category-filter/category-filter.component';
import { FilterComponent } from './components/products/filter/filter.component';
import { ChartsService } from './services/charts.service';
import { MomentModule } from 'ngx-moment';
import { ChatService } from './services/chat.service';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import {MatRadioModule} from '@angular/material/radio';
import { GoogleAnalyticalTrackerService } from './services/google-analytical-tracker.service';



@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    RegistrationComponent,
    HomeComponent,
    LoginComponent,
    ProfileComponent,
    UserdetailsComponent,
    ReviewsComponent,
    UserproductComponent,
    ProductCardComponent,
    RatingComponent,
    AddProductComponent,
    ListProductsComponent,
    ProductDetailsComponent,
    ChatComponent,
    ResetPasswordComponent,
    EmailSentComponent,
    UserSettingsComponent,
    BuyersComponent,
        RatingDailogComponent,
        SuggestionsComponent,
        UserReviewsComponent,
        FooterComponent,
        WishlistComponent,
        FooterComponent,
        CategoryFilterComponent,
    FilterComponent
        
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    MatInputModule,
    BrowserAnimationsModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule,
    ToastrModule.forRoot(),
    RouterModule,
    AppRoutingModule,
    MatCardModule,
    MatDividerModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatDialogModule,
    MatIconModule,
    ReactiveFormsModule,
    ImageCropperModule,
    MatAutocompleteModule,
    MatStepperModule,
    AvatarModule,
    ChartsModule,
    MomentModule,
    Ng2SearchPipeModule,
    MatRadioModule
    
  ],
  providers: [
     
        [AreasService, CitiesService, UserInfoService,ProductService,BuyerService,WishlistService,ChartsService,ChatService,GoogleAnalyticalTrackerService],
        UserService,
        SuggestionsService,
      AuthGuard,
      ProductOwnerGuard,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true
        },
      { provide: APP_BASE_HREF, useValue: '/' },
     
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
