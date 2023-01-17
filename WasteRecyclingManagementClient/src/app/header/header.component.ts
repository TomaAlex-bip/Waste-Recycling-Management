import { map, Observable } from 'rxjs';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { IRecyclingPoint } from '../objects/IRecyclingPoint';
import { FormControl } from '@angular/forms';
import { LocationsService } from '../shared/services/locations.service';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  title: string = "Waste Recycling Management";

  myControl = new FormControl<string | IRecyclingPoint>('');
  options: IRecyclingPoint[] = [];
  filteredOptions: Observable<IRecyclingPoint[]> | undefined;

  selectedRecyclingPointSearch: any;

  @Input() isLogged: boolean = false;
  @Input() username: string = "";

  @Output() menuClicked: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() loginClicked: EventEmitter<void> = new EventEmitter<void>();
  @Output() logoutClicked: EventEmitter<void> = new EventEmitter<void>();
  @Output() searchClicked: EventEmitter<IRecyclingPoint> = new EventEmitter<IRecyclingPoint>();

  constructor(private locationsService: LocationsService) { }

  async ngOnInit() {

    let response = await this.locationsService.getRecyclingPoints();
    response.subscribe(
      locations => {
        this.options = locations;
      }
    );

    this.filteredOptions = this.myControl.valueChanges.pipe(
      map(value => {
        const name = typeof value === 'string' ? value : value?.name;
        return name ? this._filter(name as string) : this.options.slice();
      }),
    );
  }

  onLocationSearchClick() {
    // console.log(this.selectedRecyclingPointSearch);
    if(this.selectedRecyclingPointSearch.id != undefined) {
      this.searchClicked.emit(this.selectedRecyclingPointSearch);
    }
  }

  displayFn(recyclingPoint: IRecyclingPoint): string {
    return recyclingPoint && recyclingPoint.name ? recyclingPoint.name : '';
  }

  private _filter(name: string): IRecyclingPoint[] {
    const filterValue = name.toLowerCase();

    return this.options.filter(option => option.name.toLowerCase().includes(filterValue));
  }

  openSideMenu(): void {
    this.menuClicked.emit(true);
  }

  openLoginForm(): void {
    this.loginClicked.emit();
  }

  logoutUser(): void {
    this.logoutClicked.emit();
  }

}
